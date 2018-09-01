/*==========================================================*\
 *                                                          *
 *       Script made by Manuel Rodríguez Matesanz           *
 *       for Game Makers Game Jam in 31 / 08 / 2018         *    
 *                                                          *
 *==========================================================*/

using GameJam.AudioManagement;
using GameJam.EventManagement;
using GameJam.Localization;
using GameJam.SaveData;
using GameJam.SceneManagement;
using GameJam.Setup;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Fungus;

namespace GameJam.Interface
{
    /// <summary>
    /// This class controls menu related stuff
    /// </summary>
    public class GJ_Menu : MonoBehaviour
    {
        #region Variables
        [Header("Group Reference"), Space(10)]
        /// <summary>
        /// Maingroup: New Game, Options, Credits, Exit
        /// </summary>
        [SerializeField] private GameObject m_mainGroup;
        /// <summary>
        /// Options Group: Just sound 
        /// </summary>
        [SerializeField] private GameObject m_optionsGroup;
        /// <summary>
        /// Credits pop up 
        /// </summary>
        [SerializeField] private GameObject m_creditsGroup;

        [Header("Main Group Text References"), Space(10)]
        /// <summary>
        /// Continue Game text 
        /// </summary>
        [SerializeField] private Text m_loadGameButtonText;
        /// <summary>
        /// New game text
        /// </summary>
        [SerializeField] private Text m_newGameButtonText;
        /// <summary>
        /// main options text
        /// </summary>
        [SerializeField] private Text m_optionsButtonText;
        /// <summary>
        /// Credits button text
        /// </summary>
        [SerializeField] private Text m_creditsButtonText;
        /// <summary>
        /// Exit button text
        /// </summary>
        [SerializeField] private Text m_exitButtonText;

        [Header("Options Group Text References"), Space(10)]
        /// <summary>
        /// Title for options group
        /// </summary>
        [SerializeField] private Text m_optionsTitleText;
        /// <summary>
        /// Volume Text
        /// </summary>
        [SerializeField] private Text m_volumeText;
        /// <summary>
        /// Language Text
        /// </summary>
        [SerializeField] private Text m_languageText;        
        /// <summary>
        /// Graphics Text
        /// </summary>
        [SerializeField] private Text m_graphicsText;
        /// <summary>
        /// Back text
        /// </summary>
        [SerializeField] private Text m_backText;

        [Header("Options Group UI References"), Space(10)]
        /// <summary>
        /// Main Volume Slider
        /// </summary>
        [SerializeField] private Slider m_volumeSlider;
        /// <summary>
        /// Graphics dropdown
        /// </summary>
        [SerializeField] private Dropdown m_graphicsDropdown;        
        /// <summary>
        /// Language dropdown
        /// </summary>
        [SerializeField] private Dropdown m_languagesDropdown;


        [Header("Credits Group Text References"), Space(10)]
        /// <summary>
        /// Title for credits group
        /// </summary>
        [SerializeField] private Text m_creditsTitleText;
        /// <summary>
        /// Credits
        /// </summary>
        [SerializeField] private Text m_creditsText;
        /// <summary>
        /// Back from credits popup
        /// </summary>
        [SerializeField] private Text m_creditsBackButtonText;
        #endregion

        #region Monobehaviour
        // Use this for initialization
        void Start()
        {
            StartAllListeners();
            InitializeData();
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

        #endregion

        #region Menu Methods
        /// <summary>
        /// Initialize dropdown and slider data
        /// </summary>
        public void InitializeData()
        {
            TranslateTexts();

            m_graphicsDropdown.value = QualitySettings.GetQualityLevel();

            //Parse languages
            m_languagesDropdown.ClearOptions();
            List<Dropdown.OptionData> newList = new List<Dropdown.OptionData>();
            foreach (SystemLanguage language in GJ_TextManager.Instance.ActiveLanguages)
            {
                newList.Add(new Dropdown.OptionData(language.ToString()));
            }
            m_languagesDropdown.AddOptions(newList);
            m_languagesDropdown.value = GJ_TextManager.Instance.CurrentLanguageIndex;

            // Parse Volume
            m_volumeSlider.value = GJ_AudioManager.Instance.MainVolume;

            
        }
        /// <summary>
        /// Translation of every text listened from event
        /// </summary>
        public void TranslateTexts()
        {
            // MAIN GROUP
            m_loadGameButtonText.text = GJ_TextManager.GetText(GJ_TextSetup.Menu.CONTINUE);
            m_newGameButtonText.text = GJ_TextManager.GetText(GJ_TextSetup.Menu.NEW_GAME);
            m_optionsButtonText.text = GJ_TextManager.GetText(GJ_TextSetup.Menu.OPTIONS);
            m_creditsButtonText.text = GJ_TextManager.GetText(GJ_TextSetup.Menu.CREDITS);
            m_exitButtonText.text = GJ_TextManager.GetText(GJ_TextSetup.Menu.EXIT);

            // OPTIONS GROUP
            m_optionsTitleText.text = GJ_TextManager.GetText(GJ_TextSetup.Menu.OPTIONS_TITLE);
            m_volumeText.text = GJ_TextManager.GetText(GJ_TextSetup.Menu.VOLUME);
            m_graphicsText.text = GJ_TextManager.GetText(GJ_TextSetup.Menu.GRAPHICS);
            m_languageText.text = GJ_TextManager.GetText(GJ_TextSetup.Menu.LANGUAGE);
            m_backText.text = GJ_TextManager.GetText(GJ_TextSetup.Menu.BACK);

            // CREDITS GROUP
            m_creditsTitleText.text = GJ_TextManager.GetText(GJ_TextSetup.Menu.OPTIONS_TITLE);
            m_creditsText.text = GJ_TextManager.GetText(GJ_TextSetup.Menu.CREDITS_TEXT);
            m_creditsBackButtonText.text = GJ_TextManager.GetText(GJ_TextSetup.Menu.BACK);

            // Parse quality
            string[] names = QualitySettings.names;
            m_graphicsDropdown.ClearOptions();
            List<Dropdown.OptionData> newList = new List<Dropdown.OptionData>();

            foreach (string name in names)
            {
                string n = GJ_TextManager.GetText(name);
                newList.Add(new Dropdown.OptionData(n));
            }

            m_graphicsDropdown.AddOptions(newList);
        }
        /// <summary>
        /// We check in savemanager if we have a previous game saved
        /// </summary>
        public void CheckOldSave()
        {
            m_loadGameButtonText.gameObject.SetActive(GJ_SaveManager.Instance.IsThereAnOldSave());            
        }
        /// <summary>
        /// Set volume from slider
        /// </summary>
        public void SetVolume()
        {
            GJ_AudioManager.Instance.MainVolume = m_volumeSlider.value;
        }
        /// <summary>
        /// Set the language when modifying the dropdown value
        /// </summary>
        public void SetLanguage()
        {
            GJ_TextManager.Instance.ChangeLanguageFromIndex(m_languagesDropdown.value);
        }
        /// <summary>
        /// Set graphics quality when modifying the dropdown value
        /// </summary>
        public void SetGraphics()
        {
            QualitySettings.SetQualityLevel(m_graphicsDropdown.value);
        }
        /// <summary>
        /// Start a new Game from button
        /// </summary>
        public void NewGame()
        {
            GJ_EventManager.TriggerEvent(GJ_EventSetup.SaveData.NEW_GAME);
            GJ_SceneLoader.LoadScene(GJ_SceneSetup.SCENES.MANU_TEST, 1f, GJ_EventSetup.Menu.GO_TO_GAME);
            FungusManager.Instance.MusicManager.StopMusic();
        }
        /// <summary>
        /// Continue a saved game
        /// </summary>
        public void Continue()
        {

        }
        /// <summary>
        /// Show options from main group options button
        /// </summary>
        public void ShowOptions()
        {
            m_mainGroup.SetActive(false);
            m_optionsGroup.SetActive(true);
        }
        /// <summary>
        /// Hide options from options group back button
        /// </summary>
        public void HideOptions()
        {
            m_optionsGroup.SetActive(false);
            m_mainGroup.SetActive(true);
        }
        /// <summary>
        /// Show Credits from main group credits button
        /// </summary>
        public void ShowCredits()
        {
            m_creditsGroup.SetActive(true);
            m_mainGroup.SetActive(false);
        }
        /// <summary>
        /// Hide credits from credits back button
        /// </summary>
        public void HideCredits()
        {
            m_creditsGroup.SetActive(false);
            m_mainGroup.SetActive(true);
        }
        /// <summary>
        /// Exit application from exit button
        /// </summary>
        public void ExitGame()
        {
            Application.Quit();
        }
        #endregion
    }
}
