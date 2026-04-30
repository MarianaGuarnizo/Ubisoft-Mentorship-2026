using System;
using UnityEngine;

namespace Gamekit3D.GameCommands
{
    public class SimpleTranslator : SimpleTransformer
    {
        public new Rigidbody rigidbody;
        public Vector3 start = -Vector3.forward;
        public Vector3 end = Vector3.forward;

        private bool hasPlayedSound = false; //checker si le son est déjà played

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
            }
            if (!hasPlayedSound && position > 0.01f && gameObject.name == "DoorHuge2")
            {
                AkUnitySoundEngine.PostEvent("MediumDoor", gameObject);
                hasPlayedSound = true;
            }
            if (!hasPlayedSound && position > 0.01f && gameObject.name == "DoorHuge")
            {
                AkUnitySoundEngine.PostEvent("HugeDoor", gameObject);
                hasPlayedSound = true;
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
