using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{
    private GameManager gameManager;

    [SerializeField]
    private Text ScoreText;
    [SerializeField]
    private Text ComboText;


    private void Awake()
    {
        gameManager = GameManager.Instance;
    }

    private void Update()
    {
        UpdateUI();
    }

    private void UpdateUI()
    {
        ScoreText.text = "Score: " + gameManager.Score;
        ComboText.text = "COMBO " + gameManager.scoreMultiplier + "X";

    }
}
