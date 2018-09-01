using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GameJam
{
    /// <summary>
    /// This shows a message or confirm msg showing a popup
    /// </summary>
    public class GJ_AlertManager : GJ_SingletonMonobehaviour<GJ_AlertManager>
    {
        [Header("UI References"), Space(10)]
        /// <summary>
        /// Main message group for just showing a text
        /// </summary>
        [SerializeField] private GameObject m_msgGroup;
        /// <summary>
        /// Main group message
        /// </summary>
        [SerializeField] private Text m_messageText;
        /// <summary>
        /// Confirm message group
        /// </summary>
        [SerializeField] private GameObject m_msgConfirmGroup;
        /// <summary>
        /// Confirm group message
        /// </summary>
        [SerializeField] private Text m_confirmMessageText;
        /// <summary>
        /// Options shown
        /// </summary>
        [SerializeField] private Button[] m_options;
        // Use this for initialization
        void Start()
        {

        }

        /// <summary>
        /// Instance show message
        /// </summary>
        /// <param name="_msg"></param>
        public void _ShowMessage(string _msg)
        {
            m_msgGroup.SetActive(true);
        }
        /// <summary>
        /// Hide main message pop up
        /// </summary>
        public void _HideMessage()
        {
            m_msgGroup.SetActive(false);
        }

        /// <summary>
        /// Static so we can call it easier
        /// </summary>
        /// <param name="_msg"></param>
        public static void ShowMessage(string _msg)
        {
            Instance._ShowMessage(_msg);
        }
    }
}
