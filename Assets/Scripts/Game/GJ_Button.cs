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
        [SerializeField] private GJ_Audio m_audio;
        [SerializeField] private GameObject[] gos;
        [SerializeField] private string[] messages;
        [SerializeField] private bool m_alreadyInteracted = false;
        [SerializeField] private bool m_interactOnce = true;


        // Use this for initialization
        void Start()
        {
            m_alreadyInteracted = false;
        }

        public override void Action()
        {
            m_audio.PlayThisItem();

            if ((!m_alreadyInteracted && m_interactOnce) || !m_interactOnce)
            {
                int index = 0;

                foreach (GameObject go in gos)
                {
                    go.SendMessage(messages[index]);
                    index++;
                }

                m_alreadyInteracted = true;
            }
        }
    }
}

