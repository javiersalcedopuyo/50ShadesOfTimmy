/*==========================================================*\
 *                                                          *
 *       Script made by Manuel Rodríguez Matesanz           *
 *       for Game Makers Game Jam in 31 / 08 / 2018         *    
 *                                                          *
 *==========================================================*/

using UnityEngine;

namespace GameJam
{
    /// <summary>
    /// A singleton implementation for MonoBehaviours. 
    /// </summary>
    /// <typeparam name="T">Any type we want</typeparam>
    public class GJ_SingletonMonobehaviour<T> : MonoBehaviour where T : MonoBehaviour
    {
        /// <summary>
        /// The actual instance of this type.
        /// </summary>
        private static MonoBehaviour m_instance;
        /// <summary>
        /// Do we wanna destroy this GO across levels
        /// </summary>
        protected bool m_destroyOnLoad = false;
        /// <summary>
        /// Override to maintain an instance of this object across level loads.
        /// </summary>
        public virtual bool DontDestroyOnLoad { get { return !m_destroyOnLoad; } }

        /// <summary>
        /// Called when an instance is initialized due to no previous instance found.  Use this to
		/// initialize any resources this singleton requires (eg, if this is a gui item or prefab,
        /// build out the hierarchy in here or instantiate stuff).
        /// </summary>
        protected virtual void Initialize() { }

        /// <summary>
        /// Get an instance to this MonoBehaviour.Always returns a valid object.
        /// </summary>
        public static T Instance
        {
            get
            {
                if (m_instance == null)
                {
                    // first search the scene for an instance
                    T[] scene = FindObjectsOfType<T>();

                    if (scene != null && scene.Length > 0)
                    {
                        m_instance = scene[0];

                        for (int i = 1; i < scene.Length; i++)
                        {
                            GJ_ObjectUtility.Destroy(scene[i]);
                        }
                    }
                    else
                    {
                        GameObject go = new GameObject();
                        string type_name = typeof(T).ToString();
                        int i = type_name.LastIndexOf('.') + 1;
                        go.name = (i > 0 ? type_name.Substring(i) : type_name) + " Singleton";
                        T inst = go.AddComponent<T>();
                        GJ_SingletonMonobehaviour<T> cast = inst as GJ_SingletonMonobehaviour<T>;
                        if (cast != null) cast.Initialize();
                        m_instance = (MonoBehaviour)inst;
                    }

                    if (((GJ_SingletonMonobehaviour<T>)m_instance).DontDestroyOnLoad)
                        Object.DontDestroyOnLoad(m_instance.gameObject);
                }

                return (T)m_instance;
            }
        }

        /// <summary>
        /// Return the instance if it has been initialized, null otherwise.
        /// </summary>
        public static T nullableInstance
        {
            get { return (T)m_instance; }
        }

        /// <summary>
        /// If overriding, be sure to call base.Awake().
        /// </summary>
        protected virtual void Awake()
        {
            if (m_instance == null)
            {
                m_instance = this;

                if (((GJ_SingletonMonobehaviour<T>)m_instance).DontDestroyOnLoad)
                    Object.DontDestroyOnLoad(m_instance.gameObject);
            }
            else
            {
                GJ_ObjectUtility.Destroy(this.gameObject);
            }
        }
    }
}