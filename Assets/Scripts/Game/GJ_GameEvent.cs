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
        [Header("Related GameObjects"), Space(10)]
        [SerializeField] private GameObject[] m_relatedGOs;
        [SerializeField] private string[] m_methods;
        [Header("Tag that will activate the event"), Space(10)]
        [SerializeField] private List<string> m_activatorTags;
        [Header("Properties"), Space(10)]
        [SerializeField] private bool m_onInit = false;
        [SerializeField] private bool m_playOnce = true;

        // Use this for initialization
        void Start()
        {
            if (m_onInit)
            {
                EventAction();
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (m_activatorTags.Contains(other.gameObject.tag))
            {
                EventAction();
            }
        }

        private void EventAction()
        {
            int index = 0;

            foreach (GameObject go in m_relatedGOs)
            {
                go.SendMessage(m_methods[index]);
                index++;
            }

            if (m_playOnce)
                this.gameObject.SetActive(false);//Destroy(this.gameObject);
        }
    }

}
