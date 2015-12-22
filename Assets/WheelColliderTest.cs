using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

namespace Assets
{
    public class WheelColliderTest : MonoBehaviour
    {
        private const int WindowId = 1;
        private const float Magic = 0.00000001f;
        private const float BrakeTorque = 5000f;

        [SerializeField]
        private new Rigidbody rigidbody; 
        [SerializeField]
        private WheelCollider[] wheels;
        [SerializeField]
        private Rect guiBoxRect = new Rect(10.0f, 10.0f, 300.0f, 120.0f);

        private float scalarForce;
        private bool useMagic;
        private bool useBrake;

        public void FixedUpdate()
        {
            if (useMagic)
            {
                ForEachDo(wheels, w => w.motorTorque = Magic);
            }
            else
            {
                ForEachDo(wheels, w => w.motorTorque = 0.0f);
            }

            if (useBrake)
            {
                ForEachDo(wheels, w => w.brakeTorque = BrakeTorque);
            }
            else
            {
                ForEachDo(wheels, w => w.brakeTorque = 0.0f);
            }

            Vector3 calculatedForce = CalculateForce(scalarForce);  
            rigidbody.AddForce(calculatedForce);
        }

        public void OnGUI()
        {
            guiBoxRect = GUI.Window(WindowId, guiBoxRect, WindowCallback, "Car control");
        }

        private void WindowCallback(int id)
        {
            scalarForce = GUILayout.HorizontalSlider(scalarForce, 0, 30000);
            GUILayout.Label(string.Format("{0:F2}", scalarForce));
            useMagic = GUILayout.Toggle(useMagic, "Do magic");
            useBrake = GUILayout.Toggle(useBrake, "Brake");

            GUI.DragWindow(guiBoxRect);
        }

        private Vector3 CalculateForce(float force)
        {
            return transform.forward * force;
        }

        private void ForEachDo<T>(IEnumerable<T> items, Action<T> action)
        {
            foreach (T item in items)
            {
                action(item);
            }
        }
    }
}
