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
    }

}
