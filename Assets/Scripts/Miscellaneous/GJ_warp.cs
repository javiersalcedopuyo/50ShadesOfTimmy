using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameJam.Camera;

namespace GameJam {
    public class GJ_warp : MonoBehaviour {
    
        public GameObject target;
        private float cutoff = 0.0f;
        private UnityEngine.Camera cam;

        void Start() {
            cam = UnityEngine.Camera.main;
            cutoff = 0.0f;
        }

        private void OnTriggerEnter(Collider c) {
            if(c.tag == "Player") {

                StartCoroutine("FadeOut");

                Transform exitPoint = target.transform.GetChild(0);
                c.transform.position = exitPoint.transform.position;

                //StartCoroutine("FadeIn");

            }
        }

        IEnumerator FadeOut() {

            cutoff = 0.0f;
            for (int i=0; i<30; i++) {
                Debug.LogWarning("Fading");
                cutoff += 0.034f;
                cam.GetComponent<GJ_CameraController>().transitionMat.SetFloat("_Cutoff", cutoff);
                yield return new WaitForSeconds(0.0125f);
            }
            cutoff = 1.0f;
            yield return new WaitForSeconds(0.05f);
            for (int i=0; i<30; i++) {
                Debug.LogWarning("Fading");
                cutoff -= 0.034f;
                cam.GetComponent<GJ_CameraController>().transitionMat.SetFloat("_Cutoff", cutoff);
                yield return new WaitForSeconds(0.0125f);
            }
            cutoff = 0.0f;
            yield return new WaitForSeconds(0.01f);
        }

        IEnumerator FadeIn() {
            cutoff = 1.0f;

            cutoff = 0.0f;
            yield return new WaitForSeconds(0.01f);
        }
    }
}
