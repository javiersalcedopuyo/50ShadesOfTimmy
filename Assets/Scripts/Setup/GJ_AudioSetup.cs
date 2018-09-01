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
        public enum pb_AudioTypes
        {
            MAIN,
            MUSIC,
            SFX,
            SFX2
        }
    }
}
