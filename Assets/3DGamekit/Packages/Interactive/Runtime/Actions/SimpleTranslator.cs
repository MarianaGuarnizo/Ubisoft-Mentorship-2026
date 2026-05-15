using System;
using UnityEngine;

namespace Gamekit3D.GameCommands
{
    public class SimpleTranslator : SimpleTransformer
    {
        public new Rigidbody rigidbody;
        public Vector3 start = -Vector3.forward;
        public Vector3 end = Vector3.forward;

        private bool hasPlayedSound = false; //checks if the sound is already playing

        // All portal references
        private AkRoomPortal m_CrystalroomPortal;
        private AkRoomPortal m_1stDoorPortal;
        private AkRoomPortal m_BossDoorPortal;

        new void Awake()
        {
            base.Awake();

            GameObject crystalDoor = GameObject.Find("CrystalroomDoor");
            if (crystalDoor != null)
                m_CrystalroomPortal = crystalDoor.GetComponent<AkRoomPortal>();

            GameObject firstDoor = GameObject.Find("1stDoor");
            if (firstDoor != null)
                m_1stDoorPortal = firstDoor.GetComponent<AkRoomPortal>();

            GameObject bossDoor = GameObject.Find("BossDoor");
            if (bossDoor != null)
                m_BossDoorPortal = bossDoor.GetComponent<AkRoomPortal>();
        }

        public override void PerformTransform(float position)
        {

            var curvePosition = accelCurve.Evaluate(position);
            var pos = transform.TransformPoint(Vector3.Lerp(start, end, curvePosition));
            Vector3 deltaPosition = pos - rigidbody.position;
            if (Application.isEditor && !Application.isPlaying)
                rigidbody.transform.position = pos;
            rigidbody.MovePosition(pos);

            if (m_Platform != null)
                m_Platform.MoveCharacterController(deltaPosition);

            if (!hasPlayedSound && position > 0.01f && gameObject.name == "DoorHuge1")
            {
                AkUnitySoundEngine.PostEvent("SmallDoor", gameObject);
                hasPlayedSound = true;
                //Opening AkRoomPortal for 1stDoor when the door opens
                if (m_1stDoorPortal != null)
                    m_1stDoorPortal.enabled = true;
            }
            if (!hasPlayedSound && position > 0.01f && gameObject.name == "DoorHuge2")
            {
                AkUnitySoundEngine.PostEvent("MediumDoor", gameObject);
                hasPlayedSound = true;
                //Opening AkRoomPortal for CrystalroomDoor when the door opens
                if (m_CrystalroomPortal != null)
                    m_CrystalroomPortal.enabled = true;
            }
            if (!hasPlayedSound && position > 0.01f && gameObject.name == "DoorHuge")
            {
                AkUnitySoundEngine.PostEvent("HugeDoor", gameObject);
                hasPlayedSound = true;
                //Opening AkRoomPortal for BossDoor when the door opens
                if (m_BossDoorPortal != null)
                    m_BossDoorPortal.enabled = true;
            }
            if (!hasPlayedSound && position > 0.01f && gameObject.name == "Door1")
            {
                AkUnitySoundEngine.PostEvent("SmallDoor", gameObject);
                hasPlayedSound = true;
            }
            if (!hasPlayedSound && position > 0.01f && gameObject.name == "Door2")
            {
                AkUnitySoundEngine.PostEvent("SmallDoor", gameObject);
                hasPlayedSound = true;
            }
            if (!hasPlayedSound && position > 0.01f && gameObject.name == "Door3")
            {
                AkUnitySoundEngine.PostEvent("SmallDoor", gameObject);
                hasPlayedSound = true;
            }
            if (!hasPlayedSound && position > 0.01f && gameObject.name == "Door4")
            {
                AkUnitySoundEngine.PostEvent("HugeDoor", gameObject);
                hasPlayedSound = true;
            }
            if (!hasPlayedSound && position > 0.01f && gameObject.name == "MovingPlatform22")
            {
                //Add code for PlatformFly in lvl2
                hasPlayedSound = true;
            }
            if (!hasPlayedSound && position > 0.01f && gameObject.name == "MovingPlatform23")
            {
                //Add code for PlatformFly in lvl2
                hasPlayedSound = true;
            }
        }
    }
}
