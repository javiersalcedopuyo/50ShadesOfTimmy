/*==========================================================*\
 *                                                          *
 *       Script made by Manuel Rodríguez Matesanz           *
 *       for Game Makers Game Jam in 01 / 09 / 2018         *    
 *                                                          *
 *==========================================================*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameJam.Setup
{
    /// <summary>
    /// Flowchart keys
    /// </summary>
    public static class GJ_FlowchartSetup
    {
        /// <summary>
        /// Keys for flowchart variables
        /// </summary>
        public static class Keys
        {
            public const string LANGUAGE = "Language";
        }
        /// <summary>
        /// Defined messages. Dialog msgs must be set in NPCs themselves
        /// </summary>
        public static class Messages
        {
            public const string SET_LANGUAGE = "SetLanguage";
            public const string DEAD = "DeadGame";
            public const string DOOR_CLOSED = "DoorClosed";
            public const string SIGNAL = "signal";
            public const string FIRST_CROSS = "firstCross";
            public const string NO_PRESSED_BUTTONS = "NoPressedButton";
            public const string MOVE_TUTORIAL = "MoveTutorial";
            public const string INTERACT = "Interact";
        }

    }

}
