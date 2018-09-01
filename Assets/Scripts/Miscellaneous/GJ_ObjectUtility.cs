/*==========================================================*\
 *                                                          *
 *       Script made by Manuel Rodríguez Matesanz           *
 *       for Game Makers Game Jam in 31 / 08 / 2018         *    
 *                                                          *
 *==========================================================*/

using UnityEngine;
using System.Collections;

namespace GameJam
{
    /// <summary>
    /// Some utilities for GameObjects
    /// </summary>
    public static class GJ_ObjectUtility
    {
        /// <summary>
        /// Add an empty gameObject as a child to `go`.
        /// </summary>
        /// <param name="go"></param>
        /// <returns></returns>
        public static GameObject AddChild(this GameObject go)
        {
            GameObject child = new GameObject();
            child.transform.SetParent(go.transform);
            return child;
        }
        /// <summary>
        /// Add an empty gameObject as a child to "trs".
        /// </summary>
        /// <param name="trs"></param>
        /// <returns></returns>
        public static Transform AddChild(this Transform trs)
        {
            Transform go = new GameObject().GetComponent<Transform>();
            go.SetParent(trs);
            return go;
        }
        /// <summary>
        /// Destroy gameobject instead of just deleting the component
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        public static void Destroy<T>(T obj) where T : UnityEngine.Object
        {
            GameObject.Destroy(obj);
        }
    }
}