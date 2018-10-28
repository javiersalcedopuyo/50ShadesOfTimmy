using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameJam;

namespace GameJam.Light {
    public class ConeLight : MonoBehaviour {

        private float m_angle, m_range;

        void Start () {

            m_angle = GetComponent<Light>().spotAngle;
            m_range = GetComponent<Light>().range;
        }

        public Vector3 GetLightDir() {
            return transform.forward;
        }

        public float GetSpotAngle() {
            return m_angle;
        }

        public float GetRange() {
            return m_range;
        }
    }
}