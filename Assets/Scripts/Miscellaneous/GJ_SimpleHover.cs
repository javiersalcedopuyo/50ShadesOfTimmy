using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GJ_SimpleHover : MonoBehaviour {

	[SerializeField] private float m_freq;
	[SerializeField] private float m_height;
	private float displacement;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		displacement = m_height * Mathf.Sin(Time.time * m_freq);
		transform.position += new Vector3(0.0f, displacement, 0.0f);
	}
}
