using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameJam;

namespace GameJam{

    /// <summary>
    /// This class moves, scales and rotates the collider of the platforms to
    ///     adapt them to the shadow projected by the light illuminating it.
    /// </summary>
    public class ShadowController : GJ_SingletonMonobehaviour<ShadowController> {

        private ConeLight m_light;
        private Platform m_platform;
        private Vector3 m_rayDir;
        private Vector2 m_borderAngles;	// <- This contains the angles betwen the vector connecting the light with the center of the object and the ones conencting the light with the top and right borders of the platform.
        private float m_rayAngle;	// <- This will be the absolute angle between the vector connecting the light and the center of the object, and the lookAt vector of the light.
        private float m_distL2P; // Distance between the light and the platform

        [SerializeField]
        private GameObject m_lightArray, m_platformArray;
        
        // Update is called once per frame
        void Update () {
            // Go through all lights
            for (int i=0; i<m_lightArray.transform.childCount; i++) {

                // Skip inactive lights
                if ( !m_lightArray.transform.GetChild(i).gameObject.activeInHierarchy ) continue;

                m_light = m_lightArray.transform.GetChild(i).gameObject.GetComponent<ConeLight>();

                // Go through all platforms
                // REVIEW: This could be too costly if there are too many platforms in the scene.
                for (int ii=0; ii<m_platformArray.transform.childCount; ii++) {

                    m_platform = m_platformArray.transform.GetChild(ii).gameObject.GetComponent<Platform>();

                    // Check if the platform is illuminated by the current light, to transform the collider.
                    if ( IsIlluminatedBy(m_platform, m_light, ref m_rayDir, ref m_rayAngle, ref m_distL2P) ){

                        ComputeBorderAngles( ref m_borderAngles, m_light, m_platform);
                        m_platform.MoveCollider( m_rayDir, m_borderAngles, m_light.GetRange(), m_distL2P );
                        m_platform.ResizeCollider( m_light.transform.position, m_light.GetRange() );

                    } else {
                        // If not illuminated, restore the collider
                        m_platform.MoveCollider( Vector3.zero, Vector2.zero, 0f, 0f );
                        m_platform.ResizeCollider( Vector3.zero, 0f );
                    }
                }
            }
        }

        // TODO: Add correction with the width of the object
        /// <summary>
        /// Checks if a given platform is inside the given light's cone by
        /// calculating the vector between the light and the center of the
        /// platform.
        /// If the angle between that vector and the light's forward vector, exceeds the half apperture of the light, the object is not illuminated.
        /// </summary>
        private bool IsIlluminatedBy( Platform p, ConeLight l, ref Vector3 dir, ref float angle, ref float dist) {

            /*Debug.DrawRay(l.gameObject.transform.position, 
                            Vector3.Normalize( p.GetRightBorder() - l.gameObject.transform.position) * 25f,
                            Color.red);
            Debug.DrawRay(l.gameObject.transform.position, 
                            Vector3.Normalize( p.GetTopBorder() - l.gameObject.transform.position) * 25f,
                            Color.blue);*/

            dir  = p.gameObject.transform.position - l.gameObject.transform.position;
            dist = dir.magnitude;
            dir  = Vector3.Normalize(dir);
            Vector3 lightDir = l.GetLightDir();

            angle = Mathf.Abs( Vector3.Angle(dir, lightDir) );
            float lightAngle = Mathf.Abs( l.GetSpotAngle() );

            return (angle > lightAngle*0.5f) ? false : true;
        }

        /// <summary>
        /// Computes the angle between the light forward vector and the vectors
        /// between the light and the top and right borders of the platform.
        /// </summary>
        private void ComputeBorderAngles( ref Vector2 a, ConeLight l, Platform p) {

            Vector3 vLight = p.gameObject.transform.position - l.gameObject.transform.position;
            Vector3 vRight = p.GetRightBorder() - l.gameObject.transform.position;
            Vector3 vTop   = p.GetTopBorder() - l.gameObject.transform.position;

            a.x = Vector3.Angle(vLight, vRight);
            a.y = Vector3.Angle(vLight, vTop);

            return;
        }
    }
}