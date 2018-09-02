/*==========================================================*\
 *                                                          *
 *       Script made by Manuel Rodríguez Matesanz           *
 *       for Game Makers Game Jam in 02 / 09 / 2018         *    
 *                                                          *
 *==========================================================*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameJam.Dialog;

namespace GameJam.Game
{
    public class GJ_Interactable : MonoBehaviour
    {
        [SerializeField] protected string m_dialogMessage;
        [SerializeField] protected bool m_showDialog;

        // Use this for initialization
        void Start()
        {

        }

        public virtual void ShowDialog()
        {
            if (m_showDialog)
                GJ_DialogManager.ShowDialog(m_dialogMessage);
        }

        public virtual void Action()
        {

        }
    }

}
