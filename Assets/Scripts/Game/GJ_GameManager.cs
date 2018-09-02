/*==========================================================*\
 *                                                          *
 *       Script made by Manuel Rodríguez Matesanz           *
 *       for Game Makers Game Jam in 31 / 08 / 2018         *    
 *                                                          *
 *==========================================================*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameJam.SaveData;
using GameJam.EventManagement;
using GameJam.Setup;
using Fungus;
using GameJam.Player;
using GameJam.Dialog;
using GameJam.SceneManagement;
using UnityEngine.UI;
using GameJam.Localization;

namespace GameJam.Game
{
    /// <summary>
    /// Game Manager - This controls game core 
    /// </summary>
    public class GJ_GameManager : GJ_SingletonMonobehaviour<GJ_GameManager>
    {
        [SerializeField] private GJ_AzerisController m_player;
        [SerializeField] private GJ_AzerisCharacter m_chara;
        [SerializeField] private Flowchart m_gameFlowchart;
        [SerializeField] private GJ_SaveData m_data;
        [SerializeField] private Rigidbody m_playerRb;
        [SerializeField] private Transform m_playerTrs;
        public Vector3 m_level1Pos;
        public Vector3 m_level2Pos;
        public Vector3 m_level3Pos;

        public GJ_Door m_mainDoor;
        public GameObject firstLevel;
        public GameObject firstLight;
        public GameObject finalDemoCanvas;
        public GameObject inGameMenuCanvas;

        public Text m_exitText;
        public Text m_backToMenuText;
        public Text m_ResumeText;

        public bool m_pause = false;

        public GJ_SaveData Data { get { return m_data; } }

        protected override void Awake()
        {
            m_destroyOnLoad = true;
            base.Awake();
        }

        // Use this for initialization
        void Start()
        {
            m_pause = false;
            m_data = GJ_SaveManager.Instance.Data;
            TranslateTexts();
            StartAllListeners();
        }

        void Update()
        {
            if (GJ_InputManager.PressedEscape() && !m_chara.m_movementIsBlocked)
            {
                if (!m_pause)
                {
                    Time.timeScale = 0;
                    m_pause = true;
                    CantMove();
                    inGameMenuCanvas.SetActive(true);
                }
                else
                {
                    Time.timeScale = 1;
                    m_pause = false;
                    CanMove();
                    inGameMenuCanvas.SetActive(false);
                }
            }
        }

        private void OnDestroy()
        {
            StopAllListeners();
        }

        public void Resume()
        {
            Time.timeScale = 1;
            m_pause = false;
            CanMove();
            inGameMenuCanvas.SetActive(false);
        }

        public void ExitGame()
        {
            Application.Quit();
        }

        public void TranslateTexts()
        {
            if (GJ_TextManager.Instance)
            {
                m_exitText.text = GJ_TextManager.Instance.GetTextTranslated(GJ_TextSetup.Menu.EXIT);
                m_backToMenuText.text = GJ_TextManager.Instance.GetTextTranslated(GJ_TextSetup.Menu.BACK_TO_MENU);
                m_ResumeText.text = GJ_TextManager.Instance.GetTextTranslated(GJ_TextSetup.Menu.RESUME);
            }

        }

        /// <summary>
        /// Start listening needed events
        /// </summary>
        public void StartAllListeners()
        {
            GJ_EventManager.StartListening(GJ_EventSetup.Game.DEAD, ShowDeadCanvasAndRespawn);
            GJ_EventManager.StartListening(GJ_EventSetup.Game.CAN_MOVE, CanMove);
            GJ_EventManager.StartListening(GJ_EventSetup.Localization.TRANSLATE_TEXTS, TranslateTexts);
        }
        /// <summary>
        /// Stop listening events so we avoid event leaking
        /// </summary>
        public void StopAllListeners()
        {
            GJ_EventManager.StopListening(GJ_EventSetup.Game.DEAD, ShowDeadCanvasAndRespawn);
            GJ_EventManager.StopListening(GJ_EventSetup.Game.CAN_MOVE, CanMove);
            GJ_EventManager.StopListening(GJ_EventSetup.Localization.TRANSLATE_TEXTS, TranslateTexts);
        }

        public void FirstTimeButton()
        {
            m_data.pressedFirstButton = true;
        }

        public void DialogMove()
        {
            GJ_DialogManager.ShowDialog(GJ_FlowchartSetup.Messages.MOVE_TUTORIAL);
            m_mainDoor.OpenDoor();
        }

        public void CanMove()
        {
            m_chara.m_movementIsBlocked = false;
            
        }

        public void CantMove()
        {
            m_chara.StopMove();
            m_chara.m_movementIsBlocked = true;
        }

        public void ShowDeadCanvasAndRespawn()
        {
            m_playerRb.useGravity = false;
            m_gameFlowchart.SendFungusMessage(GJ_FlowchartSetup.Messages.DEAD);
        }

        public void CloseMainDoor()
        {
            m_mainDoor.CloseDoor();
            GJ_DialogManager.ShowDialog(GJ_FlowchartSetup.Messages.INTERACT);
        }

        public void SetPlayerToOriginalPosition()
        {
            m_playerRb.useGravity = true;
            m_playerTrs.position = m_level1Pos;
            m_playerTrs.rotation = new Quaternion(0, 180, 0, 1);
        }

        public void EndDemo()
        {
            CantMove();
            m_gameFlowchart.SendFungusMessage(GJ_FlowchartSetup.Messages.END_DEMO);
        }

        public void CrossedCinematic()
        {
            m_player.EnterTheWall();
            GJ_DialogManager.ShowDialog(GJ_FlowchartSetup.Messages.FIRST_CROSS);
        }

        public void ActivateLight()
        {
            firstLight.SetActive(true);
        }

        public void BackToMenu()
        {
            Destroy(UnityEngine.Camera.main.gameObject);
            GJ_SceneLoader.LoadScene(GJ_SceneSetup.SCENES.MAIN_MENU);
        }
    }

}
