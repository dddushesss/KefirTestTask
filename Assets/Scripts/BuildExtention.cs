using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

public static partial class BuildExtention
{
    public static T GetOrAddComponent<T>(this GameObject gameObject) where T : Component
    {
        var res = gameObject.GetComponent<T>();
        if (res == null)
        {
            res = gameObject.AddComponent<T>();
        }

        return res;
    }

    public static T PopRandom<T>(this List<T> list)
    {
        var result = list[Random.Range(0, list.Count)];

        list.Remove(result);
        return result;
    }

}