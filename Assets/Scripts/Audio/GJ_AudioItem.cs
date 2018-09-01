/*==========================================================*\
 *                                                          *
 *       Script made by Manuel Rodríguez Matesanz           *
 *       for Game Makers Game Jam in 01 / 09 / 2018         *    
 *                                                          *
 *==========================================================*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameJam.Setup;

namespace GameJam.AudioManagement
{
    /// <summary>
    /// Audio Item is serialized data for Audio Manager control 
    /// This has the data needed for playing audios stored in a dictionary. So, E.G: A BGM can be set in a GO
    /// and played automatically without needing to call in ANY source without the manager knowing everytime
    /// what source it is. If that source is null, this will play in main sources audiosources
    /// </summary>
    [System.Serializable]
    public class GJ_AudioItem
    {
        /// <summary>
        /// Item key: Should be a unique key
        /// </summary>
        public string m_itemKeyInDictionary = "";
        /// <summary>
        /// Type of the audio item. if the source doesn't exists, we will use one of the main sources
        /// </summary>
        public GJ_AudioSetup.AudioTypes m_type;
        /// <summary>
        /// Clip this item will play on Play()
        /// </summary>
        public AudioClip m_clip;
        /// <summary>
        /// Source associated to this item
        /// </summary>
        public AudioSource m_source;
        /// <summary>
        /// Volume of the clip
        /// </summary>
        public float m_clipVolume = 1f;
        /// <summary>
        /// If the clip is in loop
        /// </summary>
        public bool m_inLoop = false;

        /// <summary>
        /// Constructor with all needed stuff
        /// </summary>
        /// <param name="_key"></param>
        /// <param name="_type"></param>
        /// <param name="_clip"></param>
        /// <param name="_source"></param>
        /// <param name="_inLoop"></param>
        /// <param name="_volume"></param>
        public GJ_AudioItem(string _key = "audio", GJ_AudioSetup.AudioTypes _type = GJ_AudioSetup.AudioTypes.SFX, AudioClip _clip = null,
            AudioSource _source = null, bool _inLoop = false, float _volume = 1f)
        {
            m_clip = _clip;
            m_source = _source;
            m_inLoop = _inLoop;
            m_itemKeyInDictionary = _key;
            m_clipVolume = _volume;
            m_type = _type;

        }
        /// <summary>
        /// Configure Source attached to the audioitem
        /// </summary>
        public bool ConfigureSource()
        {
            if (m_source)
            {
                m_source.clip = m_clip;
                m_source.volume = m_clipVolume;
                m_source.loop = m_inLoop;
                m_source.Stop();
                return true;
            }

            return false;
        }
        /// <summary>
        /// Play clip on the attached audiosource
        /// </summary>
        public void Play()
        {
            if (ConfigureSource())
                m_source.Play();
            else if (m_clip)
                GJ_AudioManager.Instance.PlayAudioByClip(m_type, m_clip, m_clipVolume, m_inLoop);
        }
        /// <summary>
        /// Same as play but just one shot
        /// </summary>
        public void PlayOneShot()
        {
            if (m_source)
                m_source.PlayOneShot(m_clip, m_clipVolume);
            else if (m_clip)
                GJ_AudioManager.Instance.PlayOneShotByClip(m_type, m_clip, m_clipVolume);
        }
    }
}