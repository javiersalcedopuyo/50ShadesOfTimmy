/*==========================================================*\
 *                                                          *
 *       Script made by Manuel Rodríguez Matesanz           *
 *       for Game Makers Game Jam in 02 / 09 / 2018         *    
 *                                                          *
 *==========================================================*/

using UnityEngine;
using System.Collections;

namespace GameJam.Player
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(CapsuleCollider))]
    [RequireComponent(typeof(Animator))]
    public class GJ_AzerisCharacter : MonoBehaviour
    {
        [SerializeField] float m_MovingTurnSpeed = 360;
        [SerializeField] float m_StationaryTurnSpeed = 180;
        [SerializeField] float m_MoveSpeedMultiplier = 1f;
        [SerializeField] float m_AnimSpeedMultiplier = 1f;
        [SerializeField] float m_GroundCheckDistance = 0.1f;
        [SerializeField] float m_GroundCheckDistanceMax = 1f;
        [SerializeField] float m_InteractDistance = 3f;

        [SerializeField] Rigidbody m_Rigidbody;
        [SerializeField] Animator m_Animator;
        [SerializeField] float m_OrigGroundCheckDistance;
        [SerializeField] float m_TurnAmount;
        [SerializeField] float m_ForwardAmount;
        [SerializeField] Vector3 m_GroundNormal;
        float m_CapsuleHeight;
        Vector3 m_CapsuleCenter;
        [SerializeField] CapsuleCollider m_Capsule;
        [SerializeField] int m_InteractLayer = 30;
        [SerializeField] LayerMask m_interactLayerMask = 30;
        [SerializeField] GameObject m_hitGO;


        public bool m_IsGrounded;
        public bool m_inShadows = false;
        public bool m_movementIsBlocked; //if true, all movement is blocked.


        void Start()
        {
            m_InteractLayer = (int)Mathf.Log(m_interactLayerMask.value, 2);
            m_Animator = GetComponent<Animator>();
            m_Rigidbody = GetComponent<Rigidbody>();
            m_Capsule = GetComponent<CapsuleCollider>();
            m_CapsuleHeight = m_Capsule.height;
            m_CapsuleCenter = m_Capsule.center;

            m_Rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
            m_GroundCheckDistance = m_GroundCheckDistanceMax;

            m_OrigGroundCheckDistance = m_GroundCheckDistance;
        }

        public void Move(Vector3 move, float h, float v)
        {

            if (m_movementIsBlocked) { return; }

            if (move.magnitude > 1f) move.Normalize();
            move = transform.InverseTransformDirection(move);
            CheckGroundStatus();
            move = Vector3.ProjectOnPlane(move, m_GroundNormal);
            m_TurnAmount = Mathf.Atan2(move.x, move.z);
            m_ForwardAmount = move.z;

            ApplyExtraTurnRotation();

            UpdateAnimator(move, h, v);
        }

        public void StopMove()
        {
            m_Animator.SetBool("walk", false);
            m_Animator.SetBool("run", false);
        }

        void UpdateAnimator(Vector3 move, float h, float v)
        {
            if (m_IsGrounded)
            {

                if (Mathf.Abs(h) > 0.4f || Mathf.Abs(v) > 0.4f)
                {
                    m_Animator.SetBool("walk", true);
                    m_Animator.SetBool("run", true);
                }
                else if (Mathf.Abs(h) == 0.0f && Mathf.Abs(v) == 0.0f)
                {
                    m_Animator.SetBool("walk", false);
                    m_Animator.SetBool("run", false);
                }
                else
                {
                    m_Animator.SetBool("walk", true);
                    m_Animator.SetBool("run", false);
                }
            }

            transform.Translate(move);
        }

        void ApplyExtraTurnRotation()
        {
            // help the character turn faster (this is in addition to root rotation in the animation)
            float turnSpeed = Mathf.Lerp(m_StationaryTurnSpeed, m_MovingTurnSpeed, m_ForwardAmount);
            transform.Rotate(0, m_TurnAmount * turnSpeed * Time.deltaTime, 0);
        }


        public void OnAnimatorMove()
        {
            // we implement this function to override the default root motion.
            // this allows us to modify the positional speed before it's applied.
            if (m_IsGrounded && Time.deltaTime > 0)
            {
                Vector3 v = (m_Animator.deltaPosition * m_MoveSpeedMultiplier) / Time.deltaTime;

                // we preserve the existing y part of the current velocity.
                v.y = m_Rigidbody.velocity.y;
                m_Rigidbody.velocity = v;
            }

        }

        public GameObject CheckFrontObject()
        {
            Debug.Log("checking front object");

            if (m_hitGO)
            {
                Debug.Log("Detected interactable object: " + m_hitGO.name);
                return m_hitGO;
            }

            return null;
        }

        public IEnumerator DieAndRespawn()
        {
            m_movementIsBlocked = true;
            m_Animator.SetBool("walk", false);
            m_Animator.SetBool("run", false);
            m_Animator.SetTrigger("dying");
            Move(Vector3.zero, 0, 0);
            //play scream audio
            yield return new WaitForSeconds(1f); //wait for dramatic death

            //transform.SetPositionAndRotation(data.position, Quaternion.identity);
            m_movementIsBlocked = false;
            UpdateAnimator(Vector3.zero, 0, 0);

        }

        void CheckGroundStatus()
        {
            RaycastHit hitInfo;
#if UNITY_EDITOR
            // helper to visualise the ground check ray in the scene view
            Debug.DrawLine(transform.position + (Vector3.up * 0.1f), transform.position + (Vector3.up * 0.1f) + (Vector3.down * m_GroundCheckDistance), Color.blue);
            Debug.DrawLine(transform.position + (transform.up * 1f), transform.position + (transform.up * 1f) + (transform.forward * m_InteractDistance), Color.red);
#endif

            if (Physics.Raycast(transform.position, transform.forward, out hitInfo, m_InteractDistance, m_interactLayerMask))
            {
                m_hitGO = hitInfo.transform.gameObject;
            }
            else
            {
                if (m_hitGO)
                    m_hitGO = null;
            }

            // 0.1f is a small offset to start the ray from inside the character
            // it is also good to note that the transform position in the sample assets is at the base of the character
            if (Physics.Raycast(transform.position + (Vector3.up * 0.1f), Vector3.down, out hitInfo, m_GroundCheckDistance))
            {
                m_GroundNormal = hitInfo.normal;
                m_IsGrounded = true;
                //m_Animator.applyRootMotion = true;
            }
            else
            {
                m_IsGrounded = false;
                m_GroundNormal = Vector3.up;
            }
        }
    }
}
