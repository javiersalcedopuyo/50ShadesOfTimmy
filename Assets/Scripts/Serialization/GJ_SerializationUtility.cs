/*==========================================================*\
 *                                                          *
 *       Script madodified by Manuel Rodríguez Matesanz     *
 *       originally made by Micah Paul Davis                *
 *       for Game Makers Game Jam in 31 / 08 / 2018         *    
 *                                                          *
 *==========================================================*/

using UnityEngine;
using System.Collections;
using System;
using FullSerializer;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO; //filestream
using GameJam.Setup;

namespace GameJam.Serialization
{
    /// <summary>
    /// This class serializes every object or binary to JSON
    /// and from JSON to object or binary
    /// </summary>
    public static class GJ_SerializationUtility
    {
        /*************************************
         * Save Load  object<->binary file:
         * ***********************************/
        /// <summary>
        /// Saves the object to binary file.
        /// </summary>
        /// <param name="myObjectToSave">My object to save.</param>
        /// <param name="path">Path.</param>
        /// <param name="extension">Extension.</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        public static void SaveObjectToBinaryFile<T>(T myObjectToSave, string path, string extension)
        {
            string json = SerializeToJSON(myObjectToSave);

            SaveJSONStringToBinaryFile(json, path);
        }

        /// <summary>
        /// Loads the object from binary file.
        /// </summary>
        /// <returns>The object from binary file.</returns>
        /// <param name="T">T.</param>
        /// <param name="path">Path.</param>
        public static object LoadObjectFromBinaryFile(Type T, string path)
        {
            string json = LoadJSONStringFromBinaryFile(path);
            //T o = (T) DeserializeFromJSON (typeof(T), json);
            //return o;
            return DeserializeFromJSON(T, json);

        }


        /// <summary>
        /// Loads the object from binary file.
        /// </summary>
        /// <returns>The object from binary file.</returns>
        /// <param name="path">Path.</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        public static T LoadObjectFromBinaryFile<T>(string path)
        {
            string json = LoadJSONStringFromBinaryFile(path);
            //T o = (T) DeserializeFromJSON (typeof(T), json);
            //return o;
            return (T)DeserializeFromJSON(typeof(T), json);

        }

        /*************************************
         * Full Serializer  object<->json
         * ***********************************/
        private static readonly fsSerializer _serializer = new fsSerializer();

        /// <summary>
        /// Serialize the specified type and value.
        /// returns a json string
        /// </summary>
        /// <param name="type">Type.</param>
        /// <param name="value">Value.</param>
        private static string SerializeToJSON(Type type, object value)
        {
            // serialize the data
            fsData data;
            _serializer.TrySerialize(type, value, out data).AssertSuccessWithoutWarnings();

            // emit the data via JSON
            return fsJsonPrinter.CompressedJson(data);
        }

        /// <summary>
        /// Serialize the specified type and value.
        /// returns a json string
        /// </summary>
        /// <param name="type">Type.</param>
        /// <param name="value">Value.</param>
        public static string SerializeToJSON<T>(T myObjectToSave)
        {
            // serialize the data
            fsData data;
            _serializer.TrySerialize(typeof(T), myObjectToSave, out data).AssertSuccessWithoutWarnings();

            // emit the data via JSON
            return fsJsonPrinter.CompressedJson(data);
        }

        private static object DeserializeFromJSON(Type type, string serializedState)
        {
            // step 1: parse the JSON data
            fsData data = fsJsonParser.Parse(serializedState);

            // step 2: deserialize the data
            object deserialized = null;
            _serializer.TryDeserialize(data, type, ref deserialized).AssertSuccessWithoutWarnings();

            return deserialized;
        }


        /// <summary>
        /// Deserializes object from JSONString.
        /// </summary>
        /// <returns>The object.</returns>
        /// <param name="jsonString">Json string.</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        public static T DeserializeFromJSON<T>(string jsonString)
        {

            // step 1: parse the JSON data
            fsData data = fsJsonParser.Parse(jsonString);

            // step 2: deserialize the data
            object deserialized = null;
            _serializer.TryDeserialize(data, typeof(T), ref deserialized).AssertSuccessWithoutWarnings();

            return (T)deserialized;
        }


        /*************************************
         * Binary Formatter  JSON<->object<->binary file
         * ***********************************/

        /// <summary>
        /// Saves jsonString to file in binary format.
        /// path like: "/Resources/test.json"
        /// </summary>
        /// <param name="jsonString">Json string.</param>
        /// <param name="path">Path.</param>
        public static void SaveJSONStringToBinaryFile(string jsonString, string path)
        {
            //Step 1: create formater
            BinaryFormatter bf = new BinaryFormatter();

            //Step 2: create the file and open it.
            //this is where the data is going to be saved.
            FileStream file = File.Create(path);

            //Step 4: serialize the dataContainer into the file and then close the file.
            bf.Serialize(file, new GJ_JSONData(jsonString.ToString()));
            file.Close();
        }

        /// <summary>
        /// Saves the an object to binary file, saved in a json format.
        /// </summary>
        /// <param name="myObjectToSave">My object to save.</param>
        /// <param name="path">Path.</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        public static void SaveObjectToBinaryFile<T>(T myObjectToSave, string path, bool toBinary = true)
        {

            try
            {
                if (!toBinary)
                    File.WriteAllText(path, SerializeToJSON(myObjectToSave));
                else
                    SaveJSONStringToBinaryFile(SerializeToJSON(myObjectToSave), path);

            }
            catch (System.Exception e)
            {
                Debug.LogError("Failed writing to path: " + path + "\n" + e.ToString());               
            }

        }

        /// <summary>
        /// Gets json string from file saved in binary format.
        /// </summary>
        /// <returns>The from file.</returns>
        /// <param name="path">Path.</param>
        public static string LoadJSONStringFromBinaryFile(string path)
        {
            //Step 1: Check if the file exists.
            if (File.Exists(path))
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open(path, FileMode.Open);

                //its necesary to cast the generic object into the playerdatacontainer specifically.
                GJ_JSONData data = (GJ_JSONData)bf.Deserialize(file);

                file.Close();

                //Debug.Log("unload finished succesfully");
                return data.jsonString;

            }
            return "File Doesnt exist";
        }

        /// <summary>
        /// Saves to JSON file.
        /// opens,writes,closes   Overwrites existing file.
        /// </summary>
        /// <param name="jsonData">Json data.</param>
        /// <param name="path">Path.</param>
        public static void SaveJSONStringToJSONFile(string jsonString, string path)
        {
            File.WriteAllText(path, jsonString); // opens,writes,closes   Overwrites existing file.
        }

        /// <summary>
        /// Saves ANY class to a JSON file thanks to Generic.
        /// </summary>
        /// <param name="myobject">a c# object to save.</param>
        /// <param name="path">Path.</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        public static void SaveObjectoToJSONFile<T>(T myobject, string path)
        {
            string jsonString = SerializeToJSON(myobject);
            SaveJSONStringToJSONFile(jsonString, path);
        }





        /// <summary>
        /// Loads the Json from a file.
        /// opens, reads and closes file
        /// </summary>
        /// <returns>The JSON from file.</returns>
        /// <param name="path">Path.</param>
        public static T DeserializeObjectFromJSONFile<T>(string path)
        {

            string jsonString = File.ReadAllText(path); // opens, reads and closes file
            return DeserializeFromJSON<T>(jsonString);

        }
    }
}
