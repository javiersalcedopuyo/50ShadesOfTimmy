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

namespace GameJam.Game
{
    public class GJ_GameEvent : MonoBehaviour
    {
        [SerializeField] private GameObject m_relatedGO;
        [SerializeField] private string m_method;
        [SerializeField] private bool m_onInit = false;
        // Use this for initialization
        void Start()
        {
            if (m_onInit)
            {
                m_relatedGO.SendMessage(m_method);
                Destroy(this.gameObject);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == GJ_GameSetup.Tags.PLAYER)
            {
                m_relatedGO.SendMessage(m_method);
                Destroy(this.gameObject);
            }
        }
    }

}
