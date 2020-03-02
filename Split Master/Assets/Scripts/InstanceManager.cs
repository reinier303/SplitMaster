using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstanceManager<T> : MonoBehaviour
{
    public static Dictionary<string, T> Instances {get; private set; } = new Dictionary<string, T>();

    public static void CreateInstance(string name, T script)
    {
        if(!Instances.ContainsKey(name))
        {
            Instances.Add(name, script);
        }
        else
        {
            Debug.Log(name + " Already Exists");
        }
    }

    public static void RemoveInstance(string name)
    {
        if (!Instances.ContainsKey(name))
        {
            Instances.Remove(name);
        }
        else
        {
            Debug.Log(name + " Does not exist");
        }
    }

    public static void ResetInstances()
    {
        Instances.Clear();
    }

    public static T GetInstance(string name)
    {
        return Instances[name];
    }
}
