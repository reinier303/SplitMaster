using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextDifficulty : MonoBehaviour
{
    Difficulties difficulties;
    // Start is called before the first frame update
    void Start()
    {
        difficulties = GameObject.FindGameObjectWithTag("Difficulties").GetComponent<Difficulties>();

    }

    public void Next()
    {
        difficulties.NextDifficulty();
    }
}
