using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using System.IO;

using GameJam.Setup;
using GameJam.EventManagement;
using GameJam.SceneManagement;
using GameJam;

namespace GameJam.Localization
{
    /// <summary>
    /// Text manager has every text from every language inside of its dictionary. This launches the event to translate every text!
    /// </summary>
    public class GJ_TextManager : GJ_SingletonMonobehaviour<GJ_TextManager>
    {
        #region Variables
        [Header("Localization Files"), Space(10)]
        /// <summary>
        /// Localization files (XML) for each language. The order doesn't matter.
        /// </summary>
        [SerializeField] private TextAsset[] m_localizationFiles;

        [Header("Languages"), Space(10)]
        /// <summary>
        /// Current language - Will change if pressing language button
        /// </summary>
        [SerializeField] private SystemLanguage m_currentLanguage = SystemLanguage.Spanish;
        /// <summary>
        /// Allowed languages (parsed languages)
        /// </summary>
        [SerializeField] private List<SystemLanguage> m_activelanguages;
        /// <summary>
        /// Dictionary of dictionaries with every text of every language
        /// </summary>
        private Dictionary<string, Dictionary<string, GJ_TextItem>> m_texts;
        /// <summary>
        /// Index for knowing which active language we are usen currently
        /// </summary>
        private int m_currentLanguageIndex = 0;
        /// <summary>
        /// Properties
        /// </summary>
        public List<SystemLanguage> ActiveLanguages { get { return m_activelanguages; } }
        public SystemLanguage CurrentLanguage { get { return m_currentLanguage; } }

        #endregion

        #region Monobehaviour and Initialization
        /// <summary>
        /// Awake method for singleton initialization
        /// </summary>
        private new void Awake()
        {
            base.Awake();
            // We initialize language data on start so we can call these methods in other classes Start()
            Init();
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

        }
        /// <summary>
        /// Stop Listening to events
        /// </summary>
        private void StopAllListeners()
        {

        }

        /// <summary>
        /// Manager initialization and text parse. We set the (first) current language here.
        /// </summary>
        private void Init()
        {
            StartAllListeners();
            m_texts = new Dictionary<string, Dictionary<string, GJ_TextItem>>();
            m_activelanguages = new List<SystemLanguage>();

            foreach (TextAsset a in m_localizationFiles)
            {
                ParseTexts(a);
            }

            int lastLanguage = PlayerPrefs.GetInt(GJ_GameSetup.PlayerPrefs.LAST_LANGUAGE, -1);

            if (lastLanguage != -1)
            {
                m_currentLanguage = (SystemLanguage)lastLanguage;
            }
            else
            {
                m_currentLanguage = Application.systemLanguage;

                if (!m_texts.ContainsKey(m_currentLanguage.ToString()))
                {
                    m_currentLanguage = SystemLanguage.English;
                }
            }

            m_currentLanguageIndex = m_activelanguages.IndexOf(m_currentLanguage);

            PlayerPrefs.SetInt(GJ_GameSetup.PlayerPrefs.LAST_LANGUAGE, (int)m_currentLanguage);
        }
        #endregion

        #region Language & Parse methods
        /// <summary>
        /// Method for changing the language. Calling this method will launch translate event so every text shows correctly
        /// </summary>
        /// <param name="language"></param>
        public void ChangeLanguage(SystemLanguage language)
        {
            m_currentLanguage = language;
            GJ_EventManager.TriggerEvent(GJ_EventSetup.Localization.TRANSLATE_TEXTS);
        }

        /// <summary>
        /// Method that uses System.XML to parse from XMLs localization files
        /// </summary>
        /// <param name="asset"></param>
        void ParseTexts(TextAsset asset)
        {
            Dictionary<string, GJ_TextItem> m_vTempDictionary = new Dictionary<string, GJ_TextItem>();

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(asset.text);

            string language = m_currentLanguage.ToString();
            XmlNodeList langTable = xmlDoc.GetElementsByTagName("Language");

            foreach (XmlNode lang in langTable)
            {
                language = lang.InnerText;
                try
                {
                    SystemLanguage parsedLanguage = (SystemLanguage)System.Enum.Parse(typeof(SystemLanguage), language);
                    m_activelanguages.Add(parsedLanguage);
                }
                catch (System.Exception error)
                {
                    Debug.LogError("Can't parse " + language + " to enum. Exiting the program...");
                    Application.Quit();
                }
            }

            if (m_texts.ContainsKey(language))
            {
                Debug.LogError("[Text Manager] Error! The Language should always be unique");
                Debug.LogError("Name: " + language);
                return;
            }

            XmlNodeList globalTable = xmlDoc.GetElementsByTagName("Table1");

            foreach (XmlNode TableNode in globalTable)
            {
                string newItemContext = TableNode.ChildNodes[0].InnerText;
                if (newItemContext != "")
                {
                    if (m_vTempDictionary.ContainsKey(newItemContext))
                    {
                        Debug.LogError("[UHMLXMLParse] Error! The context should always be unique (Script line " + TableNode.ChildNodes[0] + ")");
                        Debug.LogError("Name: " + newItemContext);
                    }
                    else
                    {
                        GJ_TextItem newInfoText = null;
                        //(string _key, string _text, string _audioName, bool _hasOptions, bool _showOptions)  
                        newInfoText = new GJ_TextItem(newItemContext, TableNode.ChildNodes[1].InnerText);
                        m_vTempDictionary.Add(newItemContext, newInfoText);
                    }
                }
            }

            m_texts.Add(language, m_vTempDictionary);
        }
        /// <summary>
        /// Get the text which key is defined in pb_SetupTexts
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string GetTextTranslated(string key, bool showLog = true)
        {
            string lang = m_currentLanguage.ToString();
            if (m_texts.ContainsKey(lang))
            {
                if (m_texts[lang].ContainsKey(key))
                {
                    return m_texts[lang][key].Text;
                }
                else
                {
                    if (showLog)
                        Debug.LogError("Couldn't get a translated text for " + key + " in language " + lang);
                }
            }
            else
            {
                if (showLog)
                    Debug.LogError("Language doesnt exist: " + lang);
            }

            return "";
        }
        /// <summary>
        /// Static method that calls the get text method from everywhere
        /// </summary>
        /// <param name="key"></param>
        /// <param name="showLog"></param>
        /// <returns></returns>
        public static string GetText(string key, bool showLog = true)
        {
            if (!Instance)
                return "";

            return Instance.GetTextTranslated(key, showLog);
        }


        /// <summary>
        /// Method that changes to next language (from activeLanguage's list)
        /// </summary>
        public void NextLanguage()
        {
            m_currentLanguageIndex++;
            if (m_currentLanguageIndex >= m_activelanguages.Count)
                m_currentLanguageIndex = 0;

            ChangeLanguage(m_activelanguages[m_currentLanguageIndex]);

        }

        #endregion
    }

}
