
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GameJam.Interface
{
    /// <summary>
    /// This class controls menu related stuff
    /// </summary>
    public class GJ_Menu : MonoBehaviour
    {
        #region Variables
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

        #endregion

        #region Monobehaviour
        // Use this for initialization
        void Start()
        {

        }
        #endregion

        #region Menu Methods
        /// <summary>
        /// Show options from main group options button
        /// </summary>
        public void ShowOptions()
        {
            m_optionsGroup.SetActive(true);
        }
        /// <summary>
        /// Hide options from options group back button
        /// </summary>
        public void HideOptions()
        {
            m_optionsGroup.SetActive(false);
        }
        /// <summary>
        /// Show Credits from main group credits button
        /// </summary>
        public void ShowCredits()
        {
            m_creditsGroup.SetActive(true);
        }
        /// <summary>
        /// Hide credits from credits back button
        /// </summary>
        public void HideCredits()
        {
            m_creditsGroup.SetActive(false);
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
