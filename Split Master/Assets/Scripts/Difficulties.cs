using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Difficulties : MonoBehaviour
{
    public static Difficulties Instance;

    public List<LevelData> DifficultiesList = new List<LevelData>();

    private void Awake()
    {
        //Destroy if already existing.
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Difficulties");

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);

        Instance = this;
    }

    public void NextDifficulty()
    {

    }
}
