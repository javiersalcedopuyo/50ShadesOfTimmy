using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameJam.SaveData;
using GameJam.Dialog;
//  using GameJam.Battle;

namespace GameJam.Character.Enemy{
	public class GJ_EnemyController : GJ_CharacterController {

        public int m_nextAttack;
        private int m_furyLevel, m_maxFury;
        private bool m_done;

        protected override void Start() {
            base.Start();
            m_done = false;
            m_furyLevel = 0;
            m_maxFury = Random.Range(1,5);
        }

        protected override void Update() {

            base.Update();
        }

        /*public int PrepareAttack() {

            m_nextAttack = Random.Range(0,2);
            // TODO: Play animation
            return m_nextAttack;
        }

        public void IncreaseFury() {
            m_furyLevel++;
            if (m_furyLevel >= m_maxFury) m_done = true;
        }

        public bool GaveUp() {
            return m_done;
        }*/
    }
}