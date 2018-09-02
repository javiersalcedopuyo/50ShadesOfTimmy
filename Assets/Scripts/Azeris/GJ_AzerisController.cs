/*==========================================================*\
 *                                                          *
 *       Script made by Manuel Rodríguez Matesanz           *
 *                  and Javier Salcedo                      *
 *       for Game Makers Game Jam in 31 / 08 / 2018         *    
 *                                                          *
 *==========================================================*/

using System;
using UnityEngine;
using GameJam;
using GameJam.Setup;
using GameJam.Game;
using GameJam.Dialog;

namespace GameJam.Player
{
    [RequireComponent(typeof(GJ_AzerisCharacter))]
    public class GJ_AzerisController : MonoBehaviour
    {
        private GJ_AzerisCharacter m_Character; // A reference to the ThirdPersonCharacter on the object
        [SerializeField] private Collider m_collider;
        [SerializeField] private Vector3 m_Move;
        [SerializeField] private float m_MoveMultiplier = 0.25f;
        [SerializeField] private ParticleSystem m_smoke;
        [SerializeField] private Renderer[] renderers;

        public GJ_AzerisCharacter Character { get { return m_Character; } }

        private void Start()
        {
            m_smoke.Stop();

            // get the third person character ( this should never be null due to require component )
            if (!m_Character)
                m_Character = GetComponent<GJ_AzerisCharacter>();

            if (!m_collider)
                m_collider = GetComponent<CapsuleCollider>();

        }
        private void Update()
        {
            if (GJ_InputManager.PressedInteract())
            {
                Debug.Log("Interact button pressed");

                if (!m_Character.m_inShadows)
                {
                    GameObject go = m_Character.CheckFrontObject();

                    if (go)
                    {
                        if (go.tag == GJ_GameSetup.Tags.SHAWDOW_WALL)
                        {
                            if (GJ_GameManager.Instance.Data.pressedFirstButton)
                                EnterTheWall();
                            else
                                GJ_DialogManager.ShowDialog(GJ_FlowchartSetup.Messages.NO_PRESSED_BUTTONS);
                        }
                        GJ_Interactable interactable = go.GetComponent<GJ_Interactable>();
                        if (interactable)
                        {
                            interactable.Action();
                        }
                    }
                }
                else
                {
                    GJ_SceneController.Instance.SwitchColliders();
                    EnterTheWall();
                }
            }
        }

        public void EnterTheWall()
        {
            m_smoke.Play();

            GJ_SceneController.Instance.SwitchColliders();

            m_Character.m_inShadows = !m_Character.m_inShadows;
            foreach (Renderer r in renderers)
            {
                switch (r.shadowCastingMode)
                {
                    case UnityEngine.Rendering.ShadowCastingMode.On:
                        r.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly;
                        break;

                    case UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly:
                        r.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
                        break;
                }
            }


        }


        // Fixed update is called in sync with physics
        private void FixedUpdate()
        {
            if (m_Character.m_movementIsBlocked )
                return;

            float h = GJ_InputManager.LeftJoystickHorizontal();
            float v = m_Character.m_inShadows ? 0 : GJ_InputManager.LeftJoystickVertical();

            m_Move = v * Vector3.forward + h * Vector3.right;
            m_Move *= m_MoveMultiplier;

            if ((h != 0 || v != 0) && !checkMoveableTerrain(transform.position + transform.up, m_Move, 1.5f))
            {
                m_Character.Move(Vector3.zero, 0, 0);
                return;
            }


            // pass all parameters to the character control script
            m_Character.Move(m_Move, h, v);

        }



        bool checkMoveableTerrain(Vector3 position, Vector3 desiredDirection, float distance)
        {
            Ray myRay = new Ray(position, desiredDirection); // cast a Ray from the position of our gameObject into our desired direction. Add the slopeRayHeight to the Y parameter.

            RaycastHit hit;

            Debug.DrawRay(position, desiredDirection, Color.red);

            if (Physics.Raycast(myRay, out hit, distance))
            {
                if (hit.collider.gameObject.tag == "Ground") // Our Ray has hit the ground
                {
                   
                }

            }

            return true;
        }


    }
}
