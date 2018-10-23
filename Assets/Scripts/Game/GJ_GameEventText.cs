using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameJam.Game
{
    public class GJ_GameEventText : GJ_Interactable
    {
        public bool m_ShowOnAction = false;

        public void Start()
        {
            m_showDialog = true;
        }

        public override void Action()
        {
            Debug.Log("show Text");
            base.ShowDialog();
        }

        public override void ShowDialog()
        {
            base.ShowDialog();
        }
        
    }

}
