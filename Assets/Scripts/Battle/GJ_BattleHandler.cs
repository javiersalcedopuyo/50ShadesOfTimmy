using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameJam.Setup;
using GameJam.Localization;

namespace GameJam.Battle
{
    /// <summary>
    /// Battle handler - This script controls the battle
    /// </summary>
    public class GJ_BattleHandler : MonoBehaviour
    {
        /// <summary>
        /// Who won the battle
        /// </summary>
        [SerializeField] private GJ_BattleSetup.BattleWinner m_winner = GJ_BattleSetup.BattleWinner.None;
        /// <summary>
        /// Current thing the player is doing
        /// </summary>
        [SerializeField] private GJ_BattleSetup.BattleChoices m_currentChoice = GJ_BattleSetup.BattleChoices.MAIN;


        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }

}
