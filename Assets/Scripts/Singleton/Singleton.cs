using System;
using UnityEngine;

namespace Singleton
{
    public class Singleton<T> where T : MonoBehaviour, ISingleton
    {
        private static T _instance;
        

        public static T Instance
        {
            get
            {
                if (_instance is null)
                    Init();
                return _instance;
            }
        }

        public static bool IsInited { get; set; }

        private Singleton()
        {
        }

        public static T Init(string name = "")
        {
            if (!Singleton<SingletonManager>.IsInited && typeof(T) != Type.GetType("Singleton.SingletonManager"))
            {
                Singleton<SingletonManager>.Init("Singletons");
                Singleton<SingletonManager>.Instance.Awoken.Add(Singleton<SingletonManager>.Instance.gameObject);
            }

            if (Singleton<SingletonManager>.IsInited)
            {
                var manager = Singleton<SingletonManager>.Instance;

                if (_instance != null)
                {
                    Debug.LogError($"Уже существует одиночка типа {typeof(T)}");
                    return _instance;
                }
                manager.UpdateTypeList();
            }

            IsInited = true;
            _instance = new GameObject(name == "" ? $"{typeof(T)}" : name).AddComponent<T>();

            if (typeof(T) == Type.GetType("Singleton.SingletonManager")) return _instance;
            
            _instance.transform.SetParent(Singleton<SingletonManager>.Instance.transform);
            Singleton<SingletonManager>.Instance.Awoken.Add(_instance.gameObject);
            return _instance;
        }
        
        
    }
}