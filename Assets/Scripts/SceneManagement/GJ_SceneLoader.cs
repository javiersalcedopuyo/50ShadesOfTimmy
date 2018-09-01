using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GameJam.Setup;
using GameJam.Localization;
using UnityEngine.SceneManagement;
using GameJam.EventManagement;

namespace GameJam.SceneManagement
{
    /// <summary>
    /// This class controls everything related to scene load and unload
    /// </summary>
    public class GJ_SceneLoader : GJ_SingletonMonobehaviour<GJ_SceneLoader>
    {
        /// <summary>
        /// Loading Screen group. 
        /// </summary>
        [SerializeField] GameObject m_loadingScreenGroup;
        /// <summary>
        /// Text shows "loading..." with an animation
        /// </summary>
        [SerializeField] Text m_loadingText;

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
        /// Load Scene from anywhere (static method)
        /// </summary>
        /// <param name="_scene"></param>
        /// <param name="_delayAfterLoading"></param>
        /// <param name="_eventName"></param>
        public void LoadScene(GJ_SceneSetup.SCENES _scene, float _delayAfterLoading = 1f, string _eventName = "")
        {
            Instance.LoadSceneAsync(_scene, _delayAfterLoading, _eventName);
        }
        /// <summary>
        /// Load Level Async so we can show the 
        /// </summary>
        /// <param name="_scene"></param>
        /// <param name="_delayAfterLoading"></param>
        /// <param name="_eventName"></param>
        public void LoadSceneAsync(GJ_SceneSetup.SCENES _scene, float _delayAfterLoading = 1f, string _eventName = "")
        {         
            StartCoroutine(LoadingScreen((int)_scene, _delayAfterLoading, _eventName));
        }
        /// <summary>
        /// Coroutine called for loading the next scene
        /// </summary>
        /// <param name="_index"></param>
        /// <param name="_delayAfterLoading"></param>
        /// <param name="_eventName"></param>
        /// <returns></returns>
        IEnumerator LoadingScreen(int _index, float _delayAfterLoading = 1f, string _eventName = "")
        {
            m_loadingScreenGroup.SetActive(true);
            m_loadingText.text = GJ_TextManager.GetText(GJ_TextSetup.LoadingScreen.LOADING_TEXT);

            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(_index);
           
            // Wait until the asynchronous scene fully loads
            while (!asyncLoad.isDone)
            {
                yield return null;
            }

            yield return new WaitForSeconds(_delayAfterLoading);
            m_loadingScreenGroup.SetActive(false);

            if (_eventName != "")
            {
                GJ_EventManager.TriggerEvent(_eventName);
            }
        }
    }

}
