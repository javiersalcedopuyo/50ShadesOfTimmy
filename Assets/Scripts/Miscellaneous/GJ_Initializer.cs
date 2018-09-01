/*==========================================================*\
 *                                                          *
 *       Script made by Manuel Rodríguez Matesanz           *
 *       for Game Makers Game Jam in 31 / 08 / 2018         *    
 *                                                          *
 *==========================================================*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameJam.SceneManagement;
using GameJam.Setup;

namespace GameJam
{
    /// <summary>
    /// Initializer called from initializer scene. Just sends the game to main menu with managers loaded in 
    /// memory
    /// </summary>
    public class GJ_Initializer : MonoBehaviour
    {
        // Use this for initialization
        void Start()
        {
            // load menu
            //GJ_SceneLoader.LoadScene(GJ_SceneSetup.SCENES.MAIN_MENU, 1f, GJ_EventSetup.Menu.GO_TO_MAIN_MENU);
            GJ_SceneLoader.LoadScene(GJ_SceneSetup.SCENES.MAIN_MENU);
        }

    }

}
