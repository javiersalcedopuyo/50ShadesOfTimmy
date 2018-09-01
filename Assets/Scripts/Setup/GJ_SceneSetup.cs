using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameJam.Setup
{
    /// <summary>
    /// Scene setup. We will use this Scenes as reference so we don't need
    /// to hardcode the names of the scenes nor the build index.
    /// just SceneLoad(SCENES.GAME_0);
    /// </summary>
    public static class GJ_SceneSetup
    {
        public enum SCENES
        {
            MANAGER_INITIALIZER = 0,
            MAIN_MENU,
            GAME_0,
            GAME_1,
            GAME_2,
            GAME_3,
            END,
            JAVI_TEST,
            MANU_TEST
        }
       
    }

}
