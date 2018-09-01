using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameJam.Debugging
{
    /// <summary>
    /// Class for debugging purposes
    /// </summary>
    public class GJ_Debug : MonoBehaviour
    {

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (GJ_InputManager.PressedInteract())
            {
                Debug.Log("Pressed Interact");
            }
        }
    }

}
