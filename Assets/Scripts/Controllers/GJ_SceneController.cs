using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GameJam;

namespace GameJam.Game {
    public class GJ_SceneController : GJ_SingletonMonobehaviour<GJ_SceneController> {
    
        [SerializeField] private GameObject m_Container;
        private GameObject[] m_shadowPlatforms;

        private void Start() {

            int m_numShadows = m_Container.transform.childCount;
            m_shadowPlatforms = new GameObject[m_numShadows];
            
            // Populate the array and disable the colliders of the shadows
            GameObject m_child;
            for(int i=0; i<m_numShadows; i++) {

                m_child = m_Container.transform.GetChild(i).gameObject;

                if  (m_child.transform.GetComponent<Collider>())
                    m_child.transform.GetComponent<Collider>().enabled = false;

                m_shadowPlatforms[i] = m_child;
            }
        }

        public void SwitchColliders() {

            m_Container.SetActive(true);

            foreach( GameObject o in m_shadowPlatforms) {
                Collider c = o.GetComponent<Collider>();
                if (c)
                    c.enabled = !c.enabled;
            }
        }
    }
}