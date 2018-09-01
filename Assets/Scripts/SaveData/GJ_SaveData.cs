/*==========================================================*\
 *                                                          *
 *       Script made by Manuel Rodríguez Matesanz           *
 *       for Game Makers Game Jam in 31 / 08 / 2018         *    
 *                                                          *
 *==========================================================*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameJam.SaveData
{
    /// <summary>
    /// This is the game file. Will be saved in a serialized JSON and encrypted
    /// </summary>
    [System.Serializable]
    public class GJ_SaveData
    {
        /// <summary>
        /// We save player data in game data
        /// </summary>
        public GJ_PlayerData m_playerData;

        /// <summary>
        /// Main Constructor with no parameters
        /// </summary>
        public GJ_SaveData()
        {
            m_playerData = new GJ_PlayerData();
        }
    }

}
