using GameJam.Setup;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameJam.Game
{
    public class GJ_Signal : GJ_Interactable
    {
        public int level = 1;
        // Use this for initialization
        void Start()
        {
            m_dialogMessage = GJ_FlowchartSetup.Messages.SIGNAL + level;
        }

        public override void Action()
        {
            base.ShowDialog();
        }
    }

}
