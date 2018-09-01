/*==========================================================*\
 *                                                          *
 *       Script made by Manuel Rodríguez Matesanz           *
 *       for Game Makers Game Jam in 31 / 08 / 2018         *    
 *                                                          *
 *==========================================================*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameJam.EventManagement;
using GameJam.Setup;

namespace GameJam.AudioManagement
{
    /// <summary>
    /// Audio manager. Controls everything related to audios
    /// </summary>
    public class GJ_AudioManager : GJ_SingletonMonobehaviour<GJ_AudioManager>
    {
        #region Variables
        [Header("Source References"), Space(10)]
        /// <summary>
        /// AudioSources. One channel per audio type. Main is for general purposes. We won't use more than 2 
        /// </summary>
        [SerializeField] private AudioSource[] m_sources;

        [Header("Audio properties"), Space(10)]
        /// <summary>
        /// Main Volume. Modified in Options (Main Menu Scene)
        /// </summary>
        [SerializeField] private float m_mainVolume = 1f;

        [Header("Audio Items"), Space(10)]
        /// <summary>
        /// If we want to set two audios with same key (e.g: Being lazy) a string is added to the key so we can add them to the dictionary. 
        /// </summary>
        [SerializeField] private bool m_forceSameKey = false;
        /// <summary>
        /// Audios of the scene
        /// </summary>
        private Dictionary<string, GJ_AudioItem> m_audios;
        /// <summary>
        /// Properties
        /// </summary>
        public float MainVolume { get { return m_mainVolume; } set { m_mainVolume = value; PlayerPrefs.SetFloat(GJ_GameSetup.PlayerPrefs.MAIN_VOLUME, value); } }
        #endregion

        #region Monobehaviour, Initialization & Listeners
        new void Awake()
        {
            base.Awake();

            m_audios = new Dictionary<string, GJ_AudioItem>();

            if (m_sources.Length == 0)
                m_sources = GetComponents<AudioSource>();

            m_mainVolume = PlayerPrefs.GetFloat(GJ_GameSetup.PlayerPrefs.MAIN_VOLUME, 1f);
        }
        // Use this for initialization
        void Start()
        {
            Init();
            StartAllListeners();
        }
        // On destroy
        private void OnDestroy()
        {
            StopAllListeners();
        }

        /// <summary>
        /// Initialize needed variables
        /// </summary>
        private void Init()
        {

        }

        /// <summary>
        /// Start listening needed events
        /// </summary>
        public void StartAllListeners()
        {
            GJ_EventManager.StartListening(GJ_EventSetup.Audio.STOP_ALL_CHANNELS, StopPlayingSources);
            GJ_EventManager.StartListening(GJ_EventSetup.Audio.MUTE_ALL_CHANNELS, MuteAllChannels);
        }
        /// <summary>
        /// Stop listening events so we avoid event leaking
        /// </summary>
        public void StopAllListeners()
        {
            GJ_EventManager.StopListening(GJ_EventSetup.Audio.STOP_ALL_CHANNELS, StopPlayingSources);
            GJ_EventManager.StopListening(GJ_EventSetup.Audio.MUTE_ALL_CHANNELS, MuteAllChannels);
        }
        #endregion

        #region Audio methods
        /// <summary>
        /// Creates a new AudioItem and returns it so can be used
        /// </summary>
        /// <param name="type"></param>
        /// <param name="clip"></param>
        /// <param name="source"></param>
        /// <param name="name"></param>
        /// <param name="inLoop"></param>
        /// <param name="volume"></param>
        /// <returns></returns>
        public GJ_AudioItem CreateItem(bool _addIt = true, string _key = "audio", GJ_AudioSetup.AudioTypes _type = GJ_AudioSetup.AudioTypes.SFX, AudioClip _clip = null, AudioSource _source = null, bool _inLoop = false, float _volume = 1f)
        {
            if (_source == null)
                _source = m_sources[(int)_type];

            GJ_AudioItem audio = new GJ_AudioItem(_key, _type, _clip, _source, _inLoop, _volume * m_mainVolume);

            if (_addIt)
                AddAudio(audio);

            return audio;
        }
        /// <summary>
        /// Creates a new audio items and play its clip
        /// </summary>
        /// <param name="type"></param>
        /// <param name="clip"></param>
        /// <param name="source"></param>
        /// <param name="name"></param>
        /// <param name="inLoop"></param>
        /// <param name="volume"></param>
        public void CreateItemAndPlay(bool _addIt = true, string _key = "audio", GJ_AudioSetup.AudioTypes _type = GJ_AudioSetup.AudioTypes.SFX, AudioClip _clip = null, AudioSource _source = null, bool _inLoop = false, float _volume = 1f)
        {
            if (_source == null)
                _source = m_sources[(int)_type];

            GJ_AudioItem audio = CreateItem(_addIt, _key, _type, _clip, _source,_inLoop, _volume * m_mainVolume);
            audio.Play();
        }
        /// <summary>
        /// Add audio to dictionary and plays its audioclip
        /// </summary>
        /// <param name="item"></param>
        public void AddAudioAndPlay(GJ_AudioItem item)
        {
            AddAudio(item);
            item.Play();
        }
        /// <summary>
        /// Add audio item to dictionary
        /// </summary>
        /// <param name="item"></param>
        public void AddAudio(GJ_AudioItem item)
        {
            if (!m_audios.ContainsValue(item))
            {
                m_audios.Add(item.m_itemKeyInDictionary, item);

            }
            else if (m_forceSameKey)
            {
                item.m_itemKeyInDictionary += "(ForzedClone)";
                AddAudio(item);
            }

        }
        /// <summary>
        /// Remove audio from dictionary
        /// </summary>
        /// <param name="item"></param>
        public void RemoveAudio(GJ_AudioItem item)
        {
            if (m_audios.ContainsValue(item))
            {
                m_audios.Remove(item.m_itemKeyInDictionary);
            }
        }
        /// <summary>
        /// Play audio based on key
        /// </summary>
        /// <param name="key"></param>
        public void PlayAudioWithKey(string key)
        {
            if (m_audios.ContainsKey(key))
            {
                GJ_AudioItem audio = m_audios[key];
                audio.Play();
            }
            else
            {
                Debug.LogError("[Error] AudioManager doesn't contain " + key + " in the dictionary.");
            }
        }
        /// <summary>
        /// Play one shot based on key
        /// </summary>
        /// <param name="key"></param>
        public void PlayOneShotAudioByKey(string key)
        {
            if (m_audios.ContainsKey(key))
            {
                GJ_AudioItem audio = m_audios[key];
                audio.PlayOneShot();
            }
            else
            {
                Debug.LogError("[Error] AudioManager doesn't contain " + key + " in the dictionary.");
            }
        }
        /// <summary>
        /// Mute all channels
        /// </summary>
        public void MuteAllChannels()
        {
            foreach (AudioSource s in m_sources)
            {
                s.mute = !s.mute;
            }
        }
        /// <summary>
        /// Stop Playing every source
        /// </summary>
        public void StopPlayingSources()
        {
            foreach (AudioSource s in m_sources)
            {
                if (s.isPlaying)
                    s.Stop();
            }
        }
        /// <summary>
        /// Play One shot sending the clip as reference
        /// </summary>
        /// <param name="_type"></param>
        /// <param name="_clip"></param>
        /// <param name="_volume"></param>
        public void PlayOneShotByClip(GJ_AudioSetup.AudioTypes _type = GJ_AudioSetup.AudioTypes.SFX, AudioClip _clip = null, float _volume = 1f)
        {
            if (_clip)
                m_sources[(int)_type].PlayOneShot(_clip, _volume * m_mainVolume);
        }
        /// <summary>
        /// Play one shot sending the name of the clip as reference (we need to do the resource load
        /// </summary>
        /// <param name="type"></param>
        /// <param name="clipName"></param>
        /// <param name="volume"></param>
        public void PlayOneShotByName(GJ_AudioSetup.AudioTypes _type = GJ_AudioSetup.AudioTypes.SFX, string _clipPath = "", float _volume = 1f)
        {
            if (!string.IsNullOrEmpty(_clipPath))
            {
                AudioClip clip = Resources.Load<AudioClip>(_clipPath);

                if (clip)
                    m_sources[(int)_type].PlayOneShot(clip, _volume * m_mainVolume);
            }
        }
        /// <summary>
        /// Almost like one shot but can be in loop
        /// </summary>
        /// <param name="type"></param>
        /// <param name="clip"></param>
        /// <param name="volume"></param>
        /// <param name="inLoop"></param>
        public void PlayAudioByClip(GJ_AudioSetup.AudioTypes _type = GJ_AudioSetup.AudioTypes.SFX, AudioClip _clip = null, float _volume = 1f, bool _inLoop = false)
        {
            if (_clip)
            {
                AudioSource source = m_sources[(int)_type];
                source.clip = _clip;
                source.loop = _inLoop;
                source.volume = _volume * m_mainVolume;
                source.Play();
            }     
        }
        /// <summary>
        /// Like one shot but can be in loop (sending the name by name)
        /// </summary>
        /// <param name="_type"></param>
        /// <param name="_clipPath"></param>
        /// <param name="_volume"></param>
        /// <param name="_inLoop"></param>
        public void PlayAudioByName(GJ_AudioSetup.AudioTypes _type = GJ_AudioSetup.AudioTypes.SFX, string _clipPath = "", float _volume = 1f, bool _inLoop = false)
        {
            if (!string.IsNullOrEmpty(_clipPath))
            {
                AudioSource source = m_sources[(int)_type];
                AudioClip clip = Resources.Load<AudioClip>(_clipPath);
                if (clip)
                {
                    source.clip = clip;
                    source.loop = _inLoop;
                    source.volume = _volume * m_mainVolume;
                    source.Play();
                }
            }
        }
        #endregion
    }
}
