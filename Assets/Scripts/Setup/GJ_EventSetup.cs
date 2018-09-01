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
    /// Setup for event const keys
    /// </summary>
    public static class GJ_EventSetup
    {
        public static class Localization
        {
            public const string TRANSLATE_TEXTS = "translateTextsEvent";
        }

        public static class Audio
        {
            public const string STOP_ALL_CHANNELS = "stopAllChannelsEvent";
            public const string MUTE_ALL_CHANNELS = "muteAllChannelsEvent";
        }

        public static class SaveData
        {
            public const string SAVE_GAME = "saveGameEvent";
            public const string NEW_GAME = "newGameEvent";
        }

        public static class Game
        {
            public const string START_GAME = "initGameEvent";
        }
    }

}
