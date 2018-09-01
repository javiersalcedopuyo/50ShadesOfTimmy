using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace GameJam.Character.Player{
	public class GJ_PlayerController : GJ_CharacterController {

		private enum USER_INPUTS {
			MOVEMENT = 0,
			JUMP,
			INTERACTION
		}
		private int m_lastInput;

		// Update is called once per frame
		protected override void Update () {

			base.Update();
			
			if (Input.GetAxis("Vertical") != 0.0f) {
				m_verDir = new Vector3(0,0,Input.GetAxis("Vertical"));
				m_horDir = new Vector3(0.0f, 0.0f, 0.0f);
			} else if (Input.GetAxis("Horizontal") != 0.0f) {
				m_horDir = new Vector3(Input.GetAxis("Horizontal"), 0, 0);
				m_verDir = new Vector3(0.0f, 0.0f, 0.0f);
			} else {
				m_horDir = new Vector3(0.0f, 0.0f, 0.0f);
				m_verDir = new Vector3(0.0f, 0.0f, 0.0f);
			}

			// Isometric 45º movement
			m_movDir = m_horDir + m_verDir;
			Movement(m_movDir);

			// Jump
			if (Input.GetKeyDown("space") && !m_isJumping) {
				Jump();
			}
			// Interact
			if (Input.GetKeyDown("c")) {
				Debug.LogWarning("Interaction!");
			}
		}

		protected override void OnTriggerEnter(Collider c) {

			if (c.tag == "NPC") {

				Debug.LogWarning("You may speak now, mortal.");
			}
		}
	}
}