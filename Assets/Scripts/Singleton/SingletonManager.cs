using System.Collections.Generic;
using System.Linq;

using Unity.Collections;
using UnityEngine;

namespace Singleton
{
    public class SingletonManager : MonoBehaviour, ISingleton
    {
        private HashSet<GameObject> awoken;
        public HashSet<GameObject> Awoken => awoken ??= new HashSet<GameObject>();

        [SerializeField, ReadOnly] private List<string> types;

        public void UpdateTypeList()
        {
            types = awoken.Select(type => type.name).ToList();
        }

        private void OnDestroy()
        {
            types.Clear();
            awoken.Clear();
            Singleton<SingletonManager>.IsInited = false;
            foreach (var single in awoken)
            {
                Destroy(single);
            }
        }

       
    }
}