/*==========================================================*\
 *                                                          *
 *       Script made by Manuel Rodríguez Matesanz           *
 *       for Game Makers Game Jam in 02 / 09 / 2018         *    
 *                                                          *
 *==========================================================*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameJam.AudioManagement;

namespace GameJam.Game
{
    public class GJ_Button : GJ_Interactable
    {
        [Header("Pressed audio"), Space(10)]
        /// <summary>
        /// Audio played when pressing the button
        /// </summary>
        [SerializeField] private GJ_Audio m_audio;
        [Header("Related GameObjects"), Space(10)]
        /// <summary>
        /// Related gameobjects. We will use gameobject.sendMessage to call a specific method
        /// </summary>
        [SerializeField] private GameObject[] gos;
        /// <summary>
        /// messages send to each gameobject. Need to be the same length of gos
        /// </summary>
        [SerializeField] private string[] messages;
        [Header("Button Properties GameObjects"), Space(10)]
        /// <summary>
        /// If this button has be already interacted
        /// </summary>
        [SerializeField] private bool m_alreadyInteracted = false;
        /// <summary>
        /// If we can interact just once or multiple times
        /// </summary>
        [SerializeField] private bool m_interactOnce = true;

        // Use this for initialization
        void Start()
        {
            m_alreadyInteracted = false;
        }

        public override void Action()
        {
            if (m_interactOnce && m_alreadyInteracted && !m_showDialog)
            {
                m_showDialog = true;
            }

            if ((!m_alreadyInteracted && m_interactOnce) || !m_interactOnce)
            {
                m_audio.PlayThisItem();

                int index = 0;

                foreach (GameObject go in gos)
                {
                    go.SendMessage(messages[index]);
                    index++;
                }

                m_alreadyInteracted = true;
            }

            if (m_showDialog)
                ShowDialog();
        }

        public override void ShowDialog()
        {
            base.ShowDialog();
        }
    }
}

