/*==========================================================*\
 *                                                          *
 *       Script made by Manuel Rodríguez Matesanz           *
 *       for Game Makers Game Jam in 01 / 09 / 2018         *    
 *                                                          *
 *==========================================================*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameJam.AudioManagement
{
    /// <summary>
    /// Audio Class - Adds an audioItem to Audio manager automatically
    /// </summary>
    public class GJ_Audio : MonoBehaviour
    {
        /// <summary>
        /// Serialized audio item. Define in this object everything about the audio that will be played
        /// </summary>
        [SerializeField] private GJ_AudioItem m_item;
        /// <summary>
        /// If the key stored for the dictionary in audio manager will be this GO's name
        /// </summary>
        [SerializeField] private bool m_AddGOsNameToItem = false;
        /// <summary>
        /// Play the audio on start
        /// </summary>
        [SerializeField] private bool m_playItemOnInit = false;
        /// <summary>
        /// On start we create the item and adds it to main dictionary
        /// </summary>
        private void Start()
        {
            if (m_AddGOsNameToItem)
                m_item.m_itemKeyInDictionary = this.gameObject.name;

            if (m_playItemOnInit)
                GJ_AudioManager.Instance.AddAudioAndPlay(m_item);
            else
                GJ_AudioManager.Instance.AddAudio(m_item);
        }
        /// <summary>
        /// On destroy we will play
        /// </summary>
        private void OnDestroy()
        {
            if (GJ_AudioManager.Instance)
                GJ_AudioManager.Instance.RemoveAudio(m_item);
        }
        /// <summary>
        /// Play this item with its properties
        /// </summary>
        public void PlayThisItem()
        {
            m_item.Play();
        }

        public void PlayOneShotThisItem()
        {
            m_item.PlayOneShot();
        }
    }

}
