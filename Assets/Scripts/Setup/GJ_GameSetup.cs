/*==========================================================*\
 *                                                          *
 *       Script made by Manuel Rodríguez Matesanz           *
 *       for Game Makers Game Jam in 02 / 09 / 2018         *    
 *                                                          *
 *==========================================================*/

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
        public static class GameMessages
        {
            public const string OPEN_DOOR = "OpenDoor";
            public const string CLOSE_DOOR = "CloseDoor";


        }

        public static class PlayerPrefs
        {
            public const string LAST_LANGUAGE = "lastLanguagePP";
            public const string MAIN_VOLUME = "mainVolumePP";
        }

        public static class DefaultValues
        {
            public const string CHARACTER_NAME = "Ethan";
            public const int CHARACTER_LEVEL = 1;
            public const int CHARACTER_LIVES = 1;
            public const float CHARACTER_EXPERIENCE = 0;
            public const int CHARACTER_GOLD = 0;
            public const int CHARACTER_REFLEXES = 100;
            public const int CHARACTER_DEFENSES = 100;
        }

        public static class Tags
        {
            public const string PLAYER = "Player";
            public const string GROUND = "Ground";
            public const string SHAWDOW_WALL = "ShadowWall";
            public const string SWITCH = "Switch";          
            public const string BUTTON = "Button";          
            public const string SIGNAL = "Signal";          
            public const string DOOR = "Door";          
        }

        public static class Core
        {
            public const int MAX_LEVEL = 100;
        }

        public static class SaveData
        {
            public const bool ALWAYS_NEW_GAME = true;
        }
    }

}
