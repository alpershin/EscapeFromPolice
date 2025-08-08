namespace Game.Core
{
    using UnityEngine;

    public class MonoSingleton<T> : MonoBehaviour where T : class
    {
        private static T _instance;
        
        public static T Instance => _instance;
        
        protected virtual void Awake()
        {
            InitializeSingleton();
        }

        private void InitializeSingleton()
        {
            if (_instance == null)
            {
                _instance = this as T;
                Debug.Log("singleton assigned");
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}