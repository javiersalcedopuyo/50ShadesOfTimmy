using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GJ_PlayerController : MonoBehaviour {

	private Vector3 m_horDir, m_verDir, m_movDir;
	[SerializeField] private float m_speed = 10.0f;
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
		m_horDir = new Vector3(Input.GetAxis("Horizontal"), 0, -Input.GetAxis("Horizontal"));
		m_verDir = new Vector3(Input.GetAxis("Vertical"), 0, Input.GetAxis("Vertical"));
		m_movDir = m_horDir + m_verDir;
        
		if (Input.GetKeyDown("space") && m_isOnFloor) {
			m_rb.AddForce(transform.up * m_jumpForce, ForceMode.Impulse);
			m_isOnFloor = false;
		}

		transform.position += m_movDir * m_speed * Time.deltaTime;
		//m_rb.AddForce(m_movDir*m_speed*Time.deltaTime, ForceMode.Impulse);
	}

	void OnCollisionEnter (Collision hit) {

		Debug.LogWarning(hit.collider.tag);
		if(hit.collider.tag == "Surface") {
			m_isOnFloor = true;
		}
	}
}
