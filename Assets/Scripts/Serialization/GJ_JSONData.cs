/*==========================================================*\
 *                                                          *
 *       Script made by Manuel Rodríguez Matesanz           *
 *       for Game Makers Game Jam in 31 / 08 / 2018         *    
 *                                                          *
 *==========================================================*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameJam.Serialization
{
    /// <summary>
    /// JSON Data used in GJ_SerializationUtility
    /// </summary>
    [System.Serializable]
    public class GJ_JSONData
    {
        /// <summary>
        /// the main JSON
        /// </summary>
        public string jsonString;
        /// <summary>
        /// Constructor for setting the json string
        /// </summary>
        /// <param name="json"></param>
        public GJ_JSONData(string json)
        {
            jsonString = json;
        }
    }

}
