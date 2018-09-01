/*==========================================================*\
 *                                                          *
 *       Script made by Javier Salcedo                      *
 *       for Game Makers Game Jam in 01 / 09 / 2018         *    
 *                                                          *
 *==========================================================*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameJam.Character{
    public class GJ_CharacterController : MonoBehaviour {
        
        protected Vector3 m_horDir, m_verDir, m_movDir;
		[SerializeField] private float m_speed = 10.0f;
		[SerializeField] private float m_turningSpeed = 0.01f;
		[SerializeField] private float m_jumpForce = 500.0f;
		protected Rigidbody m_rb;
		protected bool m_isJumping;
        public bool m_isMoving;
        private bool m_fallen = false;
        private Animator anim;

        private Vector3 m_upDir;
        
        // Use this for initialization
        protected virtual void Start () {
			m_rb = GetComponent<Rigidbody>(); 
			m_isJumping = false;
            m_verDir = new Vector3(0.0f,0.0f,0.0f);
            m_horDir = new Vector3(0.0f,0.0f,0.0f);
            anim = GetComponent<Animator>();
		}

        protected virtual void Update() {

            /*m_upDir = transform.InverseTransformVector(Vector3.up);
            if (m_upDir.y <= 0.0f || m_fallen) {
                //m_rb.useGravity = false;
                m_fallen = true;
                GetUp();
            } else {
                //m_rb.useGravity = true;
            }*/
        }

        /// <summary>
		/// Moves the character if needed
		/// </summary>
		protected void Movement(Vector3 m_dir) {

            //m_isMoving = (m_dir == new Vector3(0.0f, 0.0f, 0.0f) && !m_isJumping) ? false : true;

			transform.position += m_dir * m_speed * Time.deltaTime;
			//m_rb.AddForce(m_movDir*m_speed*Time.deltaTime, ForceMode.Impulse);	
			FaceDirection();
		}

		/// <summary>
		/// Rotates the character to face the direction in which it's moving
		/// </summary>
		private void FaceDirection() {

            anim.SetFloat("movX", m_movDir.x);
            anim.SetFloat("movZ", m_movDir.z);

			Quaternion m_desiredRot = Quaternion.Euler(0,0,0);
			if (m_movDir.x != 0.0f) {
				m_desiredRot = Quaternion.Euler(0.0f, Mathf.Sign(m_movDir.x) * 20.0f, 0.0f);
                anim.SetBool("isWalking", true);
			}
			if (m_movDir.z != 0.0f) {
				//float m_yRot = ( Mathf.Sign(m_verDir.x) < 0.0f ) ? 180.0f : 0.0f;
				m_desiredRot = Quaternion.Euler(Mathf.Sign(m_movDir.z) * 15.0f, 0.0f, 0.0f);
                anim.SetBool("isWalking", true);
            }
            if (m_movDir == new Vector3(0,0,0)) {
				m_desiredRot = Quaternion.Euler(0.0f, 0.0f, 0.0f);
                anim.SetBool("isWalking", false);
            } 
			transform.rotation = Quaternion.Lerp(transform.rotation, m_desiredRot, Time.time*m_turningSpeed);

		}

        /// <summary>
        /// Makes the character jump
        /// </summary>
        protected void Jump() {
			Debug.LogWarning("JUMP!");
            m_rb.AddForce(transform.up * m_jumpForce, ForceMode.Impulse);
            m_isJumping = true;
        }

        /// <summary>
        /// Gets the character up after it has fallen down
        /// </summary>
        private void GetUp() {

            Quaternion m_desiredRot = Quaternion.Euler( 0.0f, 0.0f, 0.0f);
            transform.rotation = Quaternion.Lerp(transform.rotation, m_desiredRot, Time.time*0.1f);
            
            m_fallen = (transform.rotation == m_desiredRot) ? false : true;
        }

        // Manage collisions and crossings colliders
		protected virtual void OnCollisionEnter (Collision hit) {
            if(hit.collider.tag == "Surface") {
				m_isJumping = false;
			}
        }

        protected virtual void OnTriggerEnter(Collider c) {   
        }
    }

}