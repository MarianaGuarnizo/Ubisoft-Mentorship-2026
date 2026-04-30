using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gamekit3D
{
    /// <summary>
    /// Manages AkAmbient sources in the scene by proximity.
    /// Attach to the player. Sources outside the radius are silenced
    /// so they don't flood the Wwise command queue.
    /// </summary>
    public class WwiseAmbientManager : MonoBehaviour
    {
        [Tooltip("Ambient sources further than this will be stopped.")]
        public float activationRadius = 20f;

        [Tooltip("How many physics frames to skip between checks. 1 = every frame, 5 = every 5th frame.")]
        public int checkInterval = 3;

        [Tooltip("Layer mask for AkAmbient GameObjects (set to your audio layer).")]
        public LayerMask ambientLayerMask = ~0;

        // Cached collider array to avoid per-frame allocation
        private Collider[] m_HitBuffer = new Collider[32];

        // Tracks which sources are currently playing
        private HashSet<AkAmbient> m_ActiveSources = new HashSet<AkAmbient>();

        // All known ambient sources in scene — populated once on Start
        private AkAmbient[] m_AllSources;

        private int m_FrameCounter;

        void Start()
        {
            // Cache every AkAmbient in the scene once at startup.
            // If your scene streams additively, call RefreshSourceCache() after loading.
            RefreshSourceCache();
        }

        void FixedUpdate()
        {
            m_FrameCounter++;
            if (m_FrameCounter % checkInterval != 0)
                return;

            UpdateAmbientSources();
        }

        void UpdateAmbientSources()
        {
            // Find all colliders within the activation radius
            int count = Physics.OverlapSphereNonAlloc(
                transform.position,
                activationRadius,
                m_HitBuffer,
                ambientLayerMask,
                QueryTriggerInteraction.Collide
            );

            // Build a set of what should be active this frame
            HashSet<AkAmbient> shouldBeActive = new HashSet<AkAmbient>();
            for (int i = 0; i < count; i++)
            {
                AkAmbient ambient = m_HitBuffer[i].GetComponent<AkAmbient>();
                if (ambient != null)
                    shouldBeActive.Add(ambient);
            }

            // Start sources that have come into range
            foreach (AkAmbient source in shouldBeActive)
            {
                if (!m_ActiveSources.Contains(source))
                {
                    source.enabled = true;
                    m_ActiveSources.Add(source);
                }
            }

            // Stop sources that have gone out of range
            foreach (AkAmbient source in m_AllSources)
            {
                if (m_ActiveSources.Contains(source) && !shouldBeActive.Contains(source))
                {
                    source.enabled = false;
                    m_ActiveSources.Remove(source);
                }
            }
        }

        /// <summary>
        /// Call this after additive scene loads to pick up newly spawned AkAmbient sources.
        /// </summary>
        public void RefreshSourceCache()
        {
            m_AllSources = FindObjectsOfType<AkAmbient>();
        }

        void OnDrawGizmosSelected()
        {
            Gizmos.color = new Color(1f, 0.85f, 0.2f, 0.25f);
            Gizmos.DrawSphere(transform.position, activationRadius);
            Gizmos.color = new Color(1f, 0.85f, 0.2f, 0.8f);
            Gizmos.DrawWireSphere(transform.position, activationRadius);
        }
    }
}