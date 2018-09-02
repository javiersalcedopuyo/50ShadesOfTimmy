using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GJ_Flicker : MonoBehaviour {
    
    private GameObject m_go;
    private MeshRenderer m_render;
    private Transform m_child;
    private Light m_light;

    private void Start(){

        m_render = transform.GetComponent<MeshRenderer>();
        m_child = transform.GetChild(0);
        m_light = m_child.GetComponent<Light>();
        StartCoroutine("WaitRandom");        
    }

    IEnumerator WaitRandom() {


        float ran = 0.0f;
        if (m_light.enabled) {
            ran = Random.Range(0.1f, 0.75f);
        } else {
            ran = Random.Range(0.01f, 0.25f);
        }

        yield return new WaitForSeconds(ran);

        m_light.enabled = !m_light.enabled;
        m_render.enabled = !m_render.enabled;

        StartCoroutine("WaitRandom");        
    }
}