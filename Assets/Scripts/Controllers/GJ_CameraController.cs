/*==========================================================*\
 *                                                          *
 *       Script made by Javier Salcedo                      *
 *       for Game Makers Game Jam in 01 / 09 / 2018         *    
 *                                                          *
 *==========================================================*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameJam.Camera
{
	public class GJ_CameraController : GJ_SingletonMonobehaviour<GJ_CameraController>
    {

        private Quaternion m_originalRot;
        private Vector3 m_relativeDist;
        private GameObject player;
        public Material transitionMat;

        void Start() {

            player = GameObject.FindGameObjectsWithTag("Player")[0];
            m_originalRot = transform.rotation;
            //m_relativeDist = transform.position - player.transform.position;
            m_relativeDist = new Vector3(0.0f, 3.5f, -7.0f);
        }

        void Update() {

            transform.position = Vector3.Lerp(transform.position, player.transform.position+m_relativeDist, Time.deltaTime*3);
        }

        private void OnRenderImage(RenderTexture src, RenderTexture dst){
            Graphics.Blit(src, dst, transitionMat);
        }
    }
}