using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameJam.Camera{
	public class GJ_CameraController : GJ_SingletonMonobehaviour<GJ_CameraController> {

        private Quaternion m_originalRot;
        private Vector3 m_relativeDist;
        private GameObject player;

        void Start() {

            player = GameObject.FindGameObjectsWithTag("Player")[0];
            m_originalRot = transform.rotation;
            //m_relativeDist = transform.position - player.transform.position;
            m_relativeDist = new Vector3(0.0f, 2.5f, -5.0f);
        }

        void Update() {

            transform.position = Vector3.Lerp(transform.position, player.transform.position+m_relativeDist, Time.time*0.01f);
        }
    }
}