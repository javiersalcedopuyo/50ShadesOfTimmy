using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameJam.Setup
{
    /// <summary>
    /// This class stores data for strings related to textmanager dictionary keys
    /// and parsed from the language XML
    /// </summary>
    public static class GJ_TextSetup
    {
        public class LoadingScreen
        {
            public const string LOADING_TEXT = "loadingText";
        }

        public class Menu
        {
            public const string CONTINUE = "continueText";
            public const string NEW_GAME = "newGameText";
            public const string OPTIONS = "optionsText";
            public const string CREDITS = "creditsText";
            public const string EXIT = "exitText";

            public const string OPTIONS_TITLE = "optionsTitleText";
            public const string VOLUME = "optionsVolumeText";
            public const string GRAPHICS = "optionsGraphicsText";
            public const string BACK = "backText";
            public const string LANGUAGE = "languageText";

            public const string CREDITS_TITLE = "creditsTitleText";
            public const string CREDITS_TEXT = "creditsMainText";

        }
    }
}
