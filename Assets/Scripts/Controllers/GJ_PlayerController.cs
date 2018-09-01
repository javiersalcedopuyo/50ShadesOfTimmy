/*==========================================================*\
 *                                                          *
 *       Script made by Javier Salcedo                      *
 *       for Game Makers Game Jam in 31 / 08 / 2018         *    
 *                                                          *
 *==========================================================*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace GameJam.Player{
	public class GJ_PlayerController : GJ_SingletonMonobehaviour<GJ_PlayerController> {

		private Vector3 m_horDir, m_verDir, m_movDir;
		[SerializeField] private float m_speed = 10.0f;
		[SerializeField] private float m_turningSpeed = 0.01f;
		[SerializeField] private float m_jumpForce = 500.0f;
		private Rigidbody m_rb;
		private bool m_isOnFloor;

		// Use this for initialization
		void Start () {
			m_rb = GetComponent<Rigidbody>(); 
			m_isOnFloor = false;
		}
		
		// Update is called once per frame
		void Update () {

			// Isometric 45º movement
			m_horDir = new Vector3(Input.GetAxis("Horizontal")*0.5f, 0, -Input.GetAxis("Horizontal")*0.5f);
			m_verDir = new Vector3(Input.GetAxis("Vertical"), 0, Input.GetAxis("Vertical"));
			m_movDir = m_horDir + m_verDir;
			transform.position += m_movDir * m_speed * Time.deltaTime;
			//m_rb.AddForce(m_movDir*m_speed*Time.deltaTime, ForceMode.Impulse);			

			Quaternion m_maxRot;
			if (m_horDir.x != 0.0f) {
				m_maxRot = Quaternion.Euler(0.0f, Mathf.Sign(m_horDir.x) * 90.0f, 0.0f);
				transform.rotation = Quaternion.Lerp(transform.rotation, m_maxRot, Time.time * m_turningSpeed);
			}
			if (m_verDir.x != 0.0f) {

				float m_yRot = ( Mathf.Sign(m_verDir.x) < 0.0f ) ? 180.0f : 0.0f;
				m_maxRot = Quaternion.Euler(0.0f, m_yRot, 0.0f);
				transform.rotation = Quaternion.Lerp(transform.rotation, m_maxRot, Time.time * m_turningSpeed);
			}

			if (Input.GetKeyDown("space") && m_isOnFloor) {
				m_rb.AddForce(transform.up * m_jumpForce, ForceMode.Impulse);
				m_isOnFloor = false;
			}

			if (Input.GetKeyDown("q")) {
				Debug.LogWarning("Speak, mere mortal!");
			}
		}

		void OnCollisionEnter (Collision hit) {

			if(hit.collider.tag == "Surface") {
				m_isOnFloor = true;
			}
		}
	}

}