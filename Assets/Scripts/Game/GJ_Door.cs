/*==========================================================*\
 *                                                          *
 *       Script made by Manuel Rodríguez Matesanz           *
 *       for Game Makers Game Jam in 02 / 09 / 2018         *    
 *                                                          *
 *==========================================================*/

using GameJam.AudioManagement;
using GameJam.Setup;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameJam.Game
{
    public class GJ_Door : GJ_Interactable
    {
        private bool m_closed = true;
        public Animator animator;
        public Collider collider;
        public GJ_Audio openAudio;
        public GJ_Audio closeAudio;

        // Use this for initialization
        void Start()
        {
            m_dialogMessage = GJ_FlowchartSetup.Messages.DOOR_CLOSED;
        }

        public override void Action()
        {
            if (m_closed)
            {
                ShowDialog();
            }
        }

        

        public override void ShowDialog()
        {
            base.ShowDialog();
        }

        public void CloseDoor()
        {
            collider.enabled = true;
            animator.SetTrigger("CloseDoor");
            closeAudio.PlayThisItem();
        }

        public void OpenDoor()
        {
            collider.enabled = false;
            animator.SetTrigger("OpenDoor");
            openAudio.PlayThisItem();
        }
    }

}
