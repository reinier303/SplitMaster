using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuHighscoreGetter : MonoBehaviour
{
    private void Start()
    {
        GetComponent<Text>().text = "Highscore\n" + PlayerPrefs.GetFloat("Highscore");
    }
}
