using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameJam;

namespace GameJam.Light {
    public class PointLight : BaseLight {
        private void Awake () {
            m_angle = 360f;
            m_range = GetComponent<Light>().range;
            m_dir = Vector3.zero;
        }
    }
}