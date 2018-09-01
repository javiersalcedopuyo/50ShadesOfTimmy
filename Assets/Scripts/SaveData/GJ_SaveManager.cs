/*==========================================================*\
 *                                                          *
 *       Script made by Manuel Rodríguez Matesanz           *
 *       for Game Makers Game Jam in 31 / 08 / 2018         *    
 *                                                          *
 *==========================================================*/

using GameJam.EventManagement;
using GameJam.Setup;
using GameJam.Serialization;

using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace GameJam.SaveData
{
    /// <summary>
    /// This class saves the game data to a encrypted file
    /// </summary>
    public class GJ_SaveManager : GJ_SingletonMonobehaviour<GJ_SaveManager>
    {
        /// <summary>
        /// Path where the data is stored or loaded
        /// </summary>
        private string m_dataPath = "";
        /// <summary>
        /// Is there previous data stored?
        /// </summary>
        private bool m_prevData = false;
        /// <summary>
        /// Game data
        /// </summary>
        [SerializeField] private GJ_SaveData m_gameData;

        override void Awake()
        {
            base.Awake();
            InitializeData();
        }

        // Use this for initialization
        void Start()
        {

        }

        /// <summary>
        /// Start listening needed events
        /// </summary>
        public void StartAllListeners()
        {
            GJ_EventManager.StartListening(GJ_EventSetup.SaveData.SAVE_GAME, Save);
        }
        /// <summary>
        /// Stop listening events so we avoid event leaking
        /// </summary>
        public void StopAllListeners()
        {
            GJ_EventManager.StopListening(GJ_EventSetup.SaveData.SAVE_GAME, Save);
        }

        /// <summary>
        /// Initialize Data. We set the data path + file and creates dummy data for save file
        /// </summary>
        public void InitializeData()
        {
            m_dataPath = Application.dataPath + GJ_SaveSetup.DATA_PATH;
            Directory.CreateDirectory(m_dataPath);
            m_dataPath += GJ_SaveSetup.DATA_FILE;
            
            m_gameData = new GJ_SaveData();
        }
        /// <summary>
        /// Attempt to save. We need to show a pop up with "There's previous data, want to overwrite it?"
        /// </summary>
        public void Save()
        {
            if (m_prevData)
            {

            }
            else
            {
                SaveGame();
            }
        }
        /// <summary>
        /// Save the game. An NPC can just trigger the event and this will save the game data
        /// We saved to JSON File or Encrypted File
        /// </summary>
        public void SaveGame()
        {
            GJ_SerializationUtility.SaveObjectoToJSONFile(m_gameData, m_dataPath);
        }
        /// <summary>
        /// Load game from JSON File or Encrypted File
        /// </summary>
        public void LoadGame()
        {
            m_gameData = GJ_SerializationUtility.DeserializeObjectFromJSONFile<GJ_SaveData>(m_dataPath);
        }
        /// <summary>
        /// Do we have an old save?
        /// </summary>
        /// <returns>if we have an old save and we need to activate continue button</returns>
        public bool IsThereAnOldSave()
        {
            m_prevData = false;

            if (File.Exists(m_dataPath))
                m_prevData = true;

            if (GJ_GameSetup.SaveData.ALWAYS_NEW_GAME)
                m_prevData = false;

            return m_prevData;
        }
    }
}
