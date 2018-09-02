/*==========================================================*\
 *                                                          *
 *       Script made by Manuel Rodríguez Matesanz           *
 *       for Game Makers Game Jam in 31 / 08 / 2018         *    
 *                                                          *
 *==========================================================*/

using GameJam.EventManagement;
using GameJam.Setup;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameJam
{
    /// <summary>
    /// Custom Input manager so we can define here the keys for every action
    /// and then use GJ_InputManager.IsActionPressed() to check if the action 
    /// is being done. It's better than hardcoding keycodes everyewhere
    /// </summary>
    public class GJ_InputManager : GJ_SingletonMonobehaviour<GJ_InputManager>
    {

        [SerializeField] private KeyCode[] m_interactKey = { KeyCode.E, KeyCode.Joystick1Button0 };
        [SerializeField] private KeyCode[] m_menuKey = { KeyCode.Escape, KeyCode.Joystick1Button7 };

        // Use this for initialization
        void Start()
        {
            
        }

        /// <summary>
        /// When destroying the object we must stop listening events
        /// </summary>
        private void OnDestroy()
        {
            StopAllListeners();
        }
        /// <summary>
        /// Start Listening to events
        /// </summary>
        private void StartAllListeners()
        {
            GJ_EventManager.StartListening(GJ_EventSetup.Menu.GO_TO_MAIN_MENU, ShowCursor);
            GJ_EventManager.StartListening(GJ_EventSetup.Menu.GO_TO_GAME, HideCursor);
        }
        /// <summary>
        /// Stop Listening to events
        /// </summary>
        private void StopAllListeners()
        {
            GJ_EventManager.StopListening(GJ_EventSetup.Menu.GO_TO_MAIN_MENU, ShowCursor);
            GJ_EventManager.StopListening(GJ_EventSetup.Menu.GO_TO_GAME, HideCursor);
        }
        /// <summary>
        /// Show cursor when going to menu
        /// </summary>
        public void ShowCursor()
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.None;
        }
        /// <summary>
        /// Hide cursor when going to game
        /// </summary>
        public void HideCursor()
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        /// <summary>
        /// Left joystick input
        /// </summary>
        /// <returns></returns>
        private float _LeftJoystickHorizontal()
        {
            return Input.GetAxis("Horizontal");
        }
        /// <summary>
        /// Left joystick input
        /// </summary>
        /// <returns></returns>
        private float _LeftJoystickVertical()
        {
            return Input.GetAxis("Vertical");
        }
        public static float LeftJoystickHorizontal()
        {
            return Instance._LeftJoystickHorizontal();
        }
        public static float LeftJoystickVertical()
        {
            return Instance._LeftJoystickVertical();
        }
        /// <summary>
        /// If we pressed interact buttons we tell we did
        /// </summary>
        /// <returns></returns>
        private bool _PressedInteractButton()
        {
            
            foreach (KeyCode k in m_interactKey)
            {
                if (Input.GetKeyDown(k))
                {
                    return true;
                }
            }

            return false;
        }
        /// <summary>
        /// Did we press interact buttons?
        /// </summary>
        /// <returns></returns>
        public static bool PressedInteract()
        {
            return Instance._PressedInteractButton();
        }

        private bool _PressedMenuButton()
        {
            foreach (KeyCode k in m_menuKey)
            {
                if (Input.GetKeyDown(k))
                {
                    return true;
                }
            }

            return false;
        }

        public static bool PressedEscape()
        {
            return Instance._PressedMenuButton();
        }

    }
}
