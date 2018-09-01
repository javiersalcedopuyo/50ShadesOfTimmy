/*==========================================================*\
 *                                                          *
 *       Script made by Manuel Rodríguez Matesanz           *
 *       for Game Makers Game Jam in 01 / 09 / 2018         *    
 *                                                          *
 *==========================================================*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;
using GameJam.Dialog;
using GameJam.Setup;
using GameJam.SceneManagement;

namespace GameJam.Debugging
{
    /// <summary>
    /// Class for debugging purposes
    /// </summary>
    public class GJ_Debug : MonoBehaviour
    {
        public string interactMsg = "ShowDebugText";

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (GJ_InputManager.PressedInteract())
            {
                GJ_DialogManager.ShowDialog(interactMsg);
            }

            if (Input.GetKeyDown(KeyCode.Escape))
                GJ_SceneLoader.LoadScene(GJ_SceneSetup.SCENES.MAIN_MENU);
        }
    }

}
