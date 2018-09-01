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

namespace GameJam.SaveData
{
    /// <summary>
    /// Data related to the player
    /// </summary>
    [System.Serializable]
    public class GJ_PlayerData
    {
        /// <summary>
        /// Player name
        /// </summary>
        public string m_name;
        /// <summary>
        /// Player level
        /// </summary>
        public int m_level;
        /// <summary>
        /// Player life value
        /// </summary>
        public int m_lives;
        /// <summary>
        /// Player defense value
        /// </summary>
        public int m_defense;
        /// <summary>
        /// Player reflexes value
        /// </summary>
        public int m_reflexes;
        /// <summary>
        /// Player gold value
        /// </summary>
        public int m_gold;
        /// <summary>
        /// Player Gained experience
        /// </summary>
        public float m_experience;

        /// <summary>
        /// Default constructor
        /// </summary>
        public GJ_PlayerData()
        {
            m_name = GJ_GameSetup.DefaultValues.CHARACTER_NAME;
            m_level = GJ_GameSetup.DefaultValues.CHARACTER_LEVEL;
            m_lives = GJ_GameSetup.DefaultValues.CHARACTER_LIVES;
            m_defense = GJ_GameSetup.DefaultValues.CHARACTER_DEFENSES;
            m_reflexes = GJ_GameSetup.DefaultValues.CHARACTER_REFLEXES;
            m_gold = GJ_GameSetup.DefaultValues.CHARACTER_GOLD;
            m_experience = GJ_GameSetup.DefaultValues.CHARACTER_EXPERIENCE;


        }
        /// <summary>
        /// Constructor with defined values
        /// </summary>
        /// <param name="_name"></param>
        /// <param name="_level"></param>
        /// <param name="_lives"></param>
        /// <param name="_defense"></param>
        /// <param name="_reflexes"></param>
        /// <param name="_gold"></param>
        /// <param name="_experience"></param>
        public GJ_PlayerData(string _name, int _level, int _lives, int _defense, int _reflexes, int _gold, float _experience)
        {
            m_name = _name;
            m_level = _level;
            m_lives = _lives;
            m_defense = _defense;
            m_reflexes = _reflexes;
            m_gold = _gold;
            m_experience = _experience;
        }
    }
}