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
        /// Attempts to beat the game
        /// </summary>
        public int attempts;
        /// <summary>
        /// Main Constructor with no parameters
        /// </summary>
        public GJ_SaveData()
        {

        }
        /// <summary>
        /// Contructor with parameters
        /// </summary>
        /// <param name="_attepts"></param>
        public GJ_SaveData(int _attepts)
        {
            attempts = _attepts;
        }
    }

}
