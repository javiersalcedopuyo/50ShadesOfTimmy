using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameJam;

namespace GameJam.Light {
    public class BaseLight : MonoBehaviour {

        private float m_angle, m_range;
        private Vector3 m_dir;

        public Vector3 GetLightDir() {
            return m_dir;
        }

        public float GetSpotAngle() {
            return m_angle;
        }

        public float GetRange() {
            return m_range;
        }

    }
}