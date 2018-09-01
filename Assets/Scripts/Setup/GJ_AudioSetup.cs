/*==========================================================*\
 *                                                          *
 *       Script made by Manuel Rodríguez Matesanz           *
 *       for Game Makers Game Jam in 31 / 08 / 2018         *    
 *                                                          *
 *==========================================================*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameJam.Setup
{
    /// <summary>
    /// This class stores setup for audio such as types...
    /// </summary>
    public static class GJ_AudioSetup
    {
        /// <summary>
        /// Audio Types: Main = General, Music = BGM, SFX = Effects 1, SFX2 = For playing a second SFX at the same time
        /// </summary>
        public enum AudioTypes
        {
            MAIN,
            MUSIC,
            SFX,
            SFX2
        }
    }
}
