using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameJam.Setup
{
    /// <summary>
    /// here there are all the const variables for the game so they 
    /// can be hardcoded just one time and use them everywhere
    /// </summary>
    public static class GJ_GameSetup
    {
        public static class PlayerPrefs
        {
            public const string LAST_LANGUAGE = "lastLanguagePP";
        }

        public static class DefaultValues
        {
            public const string CHARACTER_NAME = "Ethan";
            public const int CHARACTER_LIVES = 3;
        }
    }

}
