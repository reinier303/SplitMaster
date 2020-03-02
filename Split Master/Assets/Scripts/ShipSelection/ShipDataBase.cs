using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipDataBase : MonoBehaviour
{
    public Dictionary<string, ScriptableShip> Ships = new Dictionary<string, ScriptableShip>();

    private void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("ShipDataBase");

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }
}
