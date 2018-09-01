using GameJam.Setup;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameJam.SaveData
{
    /// <summary>
    /// This class saves the game data to a encrypted file
    /// </summary>
    public class GJ_SaveManager : GJ_SingletonMonobehaviour<GJ_SaveManager>
    {
        // Use this for initialization
        void Start()
        {

        }
        /// <summary>
        /// Do we have an old save?
        /// </summary>
        /// <returns>if we have an old save and we need to activate continue button</returns>
        public bool IsThereAnOldSave()
        {
            bool ret = false;


            if (GJ_GameSetup.SaveData.ALWAYS_NEW_GAME)
                ret = false;

            return ret;
        }
    }
}
