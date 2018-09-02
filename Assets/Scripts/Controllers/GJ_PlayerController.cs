/*==========================================================*\
 *                                                          *
 *       Script made by Javier Salcedo                      *
 *       for Game Makers Game Jam in 31 / 08 / 2018         *    
 *                                                          *
 *==========================================================*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameJam.Dialog;
using GameJam.Game;

namespace GameJam.Character.Player{
	public class GJ_PlayerController : GJ_CharacterController {

		private bool m_playerDead;
		GJ_SceneController m_scene;
		private bool m_isShadow;
		private Transform m_smoke;
		[SerializeField] float m_movSpeed;

		protected override void Start() {
			base.Start();
			m_inBattle = false;
			m_playerDead = false;
			m_scene = GJ_SceneController.Instance;
			m_isShadow = false;
			m_smoke = transform.GetChild(0);
			m_smoke.GetComponent<ParticleSystem>().Stop();
		}


		// Update is called once per frame
		protected override void Update () {

			base.Update();
			
			m_horDir = new Vector3(Input.GetAxis("Horizontal"), 0, 0);
			m_verDir = new Vector3(0,0,Input.GetAxis("Vertical"));
			if (m_isShadow) m_verDir = new Vector3(0.0f, 0.0f, 0.0f);

			m_movDir = (m_horDir + m_verDir);
			Movement(m_movDir * m_movSpeed * 0.05f);

			// Jump
			if (Input.GetKeyDown("space") && !m_isJumping) {
				//Jump();
			}
			// Interact
			if (GJ_InputManager.PressedInteract()) {
				if( IsNearWall() ){
					EnterTheWall();
				}
			}
		}

		private bool IsNearWall() {

			bool o = false;;
			RaycastHit hit;

			// Does the ray intersect any objects excluding the player layer
			if (Physics.Raycast(transform.position, 
								new Vector3(0,0,1), 
								out hit)) {
				if (hit.collider.tag == "Wall" && hit.distance <= 1.0f) {
					//Debug.LogWarning("NEAR WALL!");
					o = true;
				}
			}
			return o;
		}

		private void EnterTheWall () {

			m_smoke.GetComponent<ParticleSystem>().Play();
			//Light l = m_scene.m_spotlights[0].GetComponent<Light>();

			// Switch the collider of the platforms
			m_scene.SwitchColliders();

			//l.enabled = !l.enabled;
			m_isShadow = !m_isShadow;
			switch (GetComponent<MeshRenderer>().shadowCastingMode) {
				case UnityEngine.Rendering.ShadowCastingMode.On:
					GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly;
					break;
					
				case UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly:
					GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
					break;
			}
		}

		/*public void BattleMove (int moveID ) {
			// TODO: Play animation
		}

		public void ReceiveHit() {

			data.m_life--;
			if (data.m_life <= 0) m_playerDead = true;
		}

		public bool IsDead() {
			return m_playerDead;
		}*/

		protected override void OnTriggerEnter(Collider c) {

			if (c.tag == "NPC") {

				Debug.LogWarning("You may speak now, mortal.");
			}
		}
	}
}