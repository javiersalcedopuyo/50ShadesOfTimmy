/*==========================================================*\
 *                                                          *
 *       Script made by Manuel Rodríguez Matesanz           *
 *       for Game Makers Game Jam in 02 / 09 / 2018         *    
 *                                                          *
 *==========================================================*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameJam.Setup;
using GameJam.EventManagement;

namespace GameJam.Game
{
    public class GJ_DeadZone : MonoBehaviour
    {
     
        // Use this for initialization
        void Start()
        {

        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == GJ_GameSetup.Tags.PLAYER)
            {
                GJ_EventManager.TriggerEvent(GJ_EventSetup.Game.DEAD);
            }
        }
    }

}
