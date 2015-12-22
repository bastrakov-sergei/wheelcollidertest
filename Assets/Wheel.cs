using UnityEngine;

namespace Assets
{
    public class Wheel : MonoBehaviour
    {                
        [SerializeField]
        private WheelCollider wheel;

        private Vector3 wheelCenter;
        private RaycastHit hit;

        public void Update()
        {
            wheelCenter = wheel.transform.TransformPoint(wheel.center);

            if (Physics.Raycast(wheelCenter, -wheel.transform.up, out hit, wheel.suspensionDistance + wheel.radius))
            {
                transform.position = hit.point + (wheel.transform.up * wheel.radius);
            }
            else
            {
                transform.position = wheelCenter - (wheel.transform.up * wheel.suspensionDistance);
            }

            transform.Rotate(wheel.rpm / 60 * 360 * Time.deltaTime, 0, 0);
        }
    }
}