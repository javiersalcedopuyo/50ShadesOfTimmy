/*==========================================================*\
 *                                                          *
 *       Script made by Manuel Rodríguez Matesanz           *
 *       for Game Makers Game Jam in 31 / 08 / 2018         *    
 *                                                          *
 *==========================================================*/

using GameJam.EventManagement;
using GameJam.Setup;
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
        private string m_dataPath;

        // Use this for initialization
        void Start()
        {
            m_dataPath = Application.dataPath + GJ_SaveSetup.DATA_PATH;
        }

        /// <summary>
        /// Start listening needed events
        /// </summary>
        public void StartAllListeners()
        {
            GJ_EventManager.StartListening(GJ_EventSetup.SaveData.SAVE_GAME, SaveGame);
        }
        /// <summary>
        /// Stop listening events so we avoid event leaking
        /// </summary>
        public void StopAllListeners()
        {
            GJ_EventManager.StopListening(GJ_EventSetup.SaveData.SAVE_GAME, SaveGame);
        }

        public void SaveGame()
        {

        }

        public void LoadGame()
        {
            
        }
        /// <summary>
        /// Do we have an old save?
        /// </summary>
        /// <returns>if we have an old save and we need to activate continue button</returns>
        public bool IsThereAnOldSave()
        {
            bool ret = false;

            if (File.Exists(m_dataPath + GJ_SaveSetup.DATA_FILE))
                ret = true;

            if (GJ_GameSetup.SaveData.ALWAYS_NEW_GAME)
                ret = false;

            return ret;
        }
    }
}
