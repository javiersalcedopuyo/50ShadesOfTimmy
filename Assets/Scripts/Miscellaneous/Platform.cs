using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameJam;

namespace GameJam.Platform {
    public class Platform : MonoBehaviour {

        private dynamic m_shadowCollider, m_collider;
        private Vector3 m_startSize, m_rightBorder, m_topBorder;

        private Vector3 debug_rGizmo, debug_tGizmo;

        private void Awake() {

            debug_rGizmo = Vector3.zero;
            debug_tGizmo = Vector3.zero;

            Collider genericCollider = transform.GetChild(0).GetComponent<Collider>();
            m_startSize = transform.GetComponent<Collider>().bounds.size;

            // Get the type of the collider and cast the generic one to it 
            // All types of collider have the center and size properties except mesh collider, so we avoid it for now.
            if ( genericCollider.GetType() == typeof(BoxCollider) ) {

                    m_shadowCollider = (BoxCollider) genericCollider;
                    m_collider = gameObject.GetComponent<BoxCollider>();

            } else if ( genericCollider.GetType() == typeof(SphereCollider) ) {

                    m_shadowCollider = (SphereCollider) genericCollider;
                    m_collider = gameObject.GetComponent<SphereCollider>();;

            } else if ( genericCollider.GetType() == typeof(CapsuleCollider) ) {
                
                    m_shadowCollider = (CapsuleCollider) genericCollider;
                    m_collider = gameObject.GetComponent<CapsuleCollider>();

            } else if ( genericCollider.GetType() == typeof(MeshCollider) ) {
                // TODO: Add MeshCollider support
                Debug.LogWarning("WARNING: MeshCollider not supported yet!");
            } else {
                Debug.LogError("ERROR: Unknown collider type!");
            }
        }

        private void Update() {
            
            // Get the aproximated center position of the object's right and top borders
            Vector3 halfSize = m_startSize * 0.5f;
            m_rightBorder = transform.position + transform.right * halfSize.x + transform.forward * halfSize.z;
            m_topBorder   = transform.position + transform.up    * halfSize.y + transform.forward * halfSize.z;
        }

        // Moves the collider to the nearest wall in the given direction and scales it to fit the shadow
        public void MoveCollider(Vector3 dir, Vector2 angle, float maxDist, float lightDist) {

            // If the direction vector is 0, it means, the platform is not illuminated, so the collider must be normal
            if (dir == Vector3.zero) {
                m_shadowCollider.center = m_collider.center;
            }

            RaycastHit[] hits;
            hits = Physics.RaycastAll(transform.position, dir, maxDist);

            // Look for the hit with a wall
            foreach(RaycastHit hit in hits) {
                if (hit.transform.gameObject.tag == "Wall") {

                    Debug.DrawRay(transform.position, dir*hit.distance, Color.yellow);

                    // Move the collider to the wall (First we need to transform it to local coordinates)
                    m_shadowCollider.center = transform.InverseTransformPoint( transform.position + dir*hit.distance );

                    // Rotate the collider to accomodate the Y rotation of the wall. This way we avoid it stepping in or out the wall.
                    Vector3 eulerRotation = transform.rotation.eulerAngles;
                    Quaternion newRotation = Quaternion.Euler( eulerRotation.x, hit.transform.rotation.y, eulerRotation.z );
                    transform.GetChild(0).transform.rotation = newRotation;
                }
            }
        }

        // TODO: Improve rotation handling
        public void ResizeCollider(Vector3 lightPos, float maxDist) {

            // If not illuminated
            if (lightPos == Vector3.zero) {
                m_shadowCollider.size = m_collider.size;
            }

            Vector3 dirX = Vector3.Normalize( m_rightBorder - lightPos );
            Vector3 dirY = Vector3.Normalize( m_topBorder   - lightPos );

            Vector3 diffPos = Vector3.zero;
            float newSizeX = 1f;
            float newSizeY = 1f;

            RaycastHit[] hits;
            hits = Physics.RaycastAll(lightPos, dirX, maxDist);
            foreach( RaycastHit h in hits ) {
                if (h.transform.gameObject.tag == "Wall"){

                    debug_rGizmo = h.point;

                    // Pass the intersection point to the collider local coordinate space and get the distance in X. (NOTE: center is already in local space)
                    // This way, we already have the scale in local coordinates.
                    newSizeX = Mathf.Abs( (m_shadowCollider.center - m_shadowCollider.transform.InverseTransformPoint(h.point)).x ) * 2f;
                }
            }

            hits = Physics.RaycastAll(lightPos, dirY, maxDist);
            foreach( RaycastHit h in hits ) {
                if (h.transform.gameObject.tag == "Wall"){

                    debug_tGizmo = h.point;
                    newSizeY = Mathf.Abs( (m_shadowCollider.center - m_shadowCollider.transform.InverseTransformPoint(h.point)).y ) * 2f;
                }
            }

            // Give the new size a small depth to allow gameObjects to be on top
            m_shadowCollider.size = new Vector3(newSizeX, newSizeY, 3f);
        }

        public Vector3 GetColliderStartSize() {
            return m_startSize;
        }

        public Vector3 GetRightBorder() {
            return m_rightBorder;
        }

        public Vector3 GetTopBorder() {
            return m_topBorder;
        }

        // For debugging purposes
        private void OnDrawGizmos() {
            
            /*Gizmos.color = Color.red;
            Gizmos.DrawSphere(m_rightBorder, 0.05f);
            Gizmos.DrawSphere(debug_rGizmo, 0.05f);

            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(m_topBorder, 0.05f);
            Gizmos.DrawSphere(debug_tGizmo, 0.05f);*/
        }
    }
}