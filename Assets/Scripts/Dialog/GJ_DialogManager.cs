/*==========================================================*\
 *                                                          *
 *       Script made by Manuel Rodríguez Matesanz           *
 *       for Game Makers Game Jam in 01 / 09 / 2018         *    
 *                                                          *
 *==========================================================*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameJam.Setup;
using Fungus;
using GameJam.EventManagement;
using GameJam.Localization;

namespace GameJam.Dialog
{
    /// <summary>
    /// Manager that controls the shown dialogs. This is called from other classes
    /// </summary>
    public class GJ_DialogManager : GJ_SingletonMonobehaviour<GJ_DialogManager>
    {
        /// <summary>
        /// Flowchart reference
        /// </summary>
        [SerializeField] private Flowchart m_flowchart;
        /// <summary>
        /// Localization component
        /// </summary>
        [SerializeField] private Fungus.Localization m_localization;

        protected override void Awake()
        {
            m_destroyOnLoad = true;
            base.Awake();
            TranslateTexts();
        }

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
            GJ_EventManager.StartListening(GJ_EventSetup.Localization.TRANSLATE_TEXTS, TranslateTexts);
        }
        /// <summary>
        /// Stop Listening to events
        /// </summary>
        private void StopAllListeners()
        {
            GJ_EventManager.StopListening(GJ_EventSetup.Localization.TRANSLATE_TEXTS, TranslateTexts);
        }

        /// <summary>
        /// TRanslate flowchart texts with set language
        /// </summary>
        void TranslateTexts()
        {

            string lang = GJ_TextManager.Instance.CurrentLanguage.ToString();
            //m_flowchart.SetStringVariable(GJ_FlowchartSetup.Keys.LANGUAGE, lang);
            //m_flowchart.SendFungusMessage(GJ_FlowchartSetup.Messages.SET_LANGUAGE);

            m_localization.SetActiveLanguage(lang, true);

            Debug.Log("Lang is " + lang + " local: " + m_localization.ActiveLanguage);

        }

        /// <summary>
        ///  Sends a message to flowchart so a dialog is shown
        /// </summary>
        /// <param name="_messageName"></param>
        private void _showDialog(string _messageName)
        {
            m_flowchart.SendFungusMessage(_messageName);
        }
        /// <summary>
        /// Calls the isntance method to send a message to flowchart so a dialog is shown
        /// </summary>
        /// <param name="_key"></param>
        public static void ShowDialog(string _messageName)
        {
            Instance._showDialog(_messageName);
        }
    }

}
