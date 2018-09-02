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
        public GameObject m_player;
        public Quaternion m_originalRot;
        public Vector3 m_relativeDist = new Vector3(0.0f, 3.5f, -7.0f);
        public Material transitionMat;


        void Start()
        {
            if (!m_player)
                m_player = GameObject.FindGameObjectsWithTag("Player")[0];
        }

        void Update()
        {

            transform.position = Vector3.Lerp(transform.position, m_player.transform.position + m_relativeDist, Time.deltaTime * 3);
        }

        private void OnRenderImage(RenderTexture src, RenderTexture dst)
        {
            Graphics.Blit(src, dst, transitionMat);
        }
    }
}