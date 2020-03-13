using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    private GameManager gameManager;

    [SerializeField]
    private Text ComboText, ScoreText, percentText, powerUpText;
    [SerializeField]
    private Slider progressBar, powerUpBar;
    public Coroutine runningRoutine;

    private void Awake()
    {
        Instance = this;
        gameManager = GameManager.Instance;
        progressBar.maxValue = gameManager.totalAmount;
    }

    private void Update()
    {
        UpdateUI();
    }

    private void UpdateUI()
    {
        ScoreText.text = "Score: " + gameManager.Score;
        ComboText.text = "COMBO " + gameManager.scoreMultiplier + "X";
        progressBar.value = gameManager.totalKills;
        percentText.text = Mathf.FloorToInt((gameManager.totalKills/gameManager.totalAmount) * 100)+ "%";
    }

    public IEnumerator StartPowerUpTimer(float time, string powerUpName)
    {
        float timer = 0;
        powerUpBar.gameObject.SetActive(true);
        powerUpBar.maxValue = time;
        powerUpText.text = powerUpName;
        while (timer <= time)
        {
            timer += Time.deltaTime;
            powerUpBar.value = timer;
            yield return new WaitForEndOfFrame();
        }
        powerUpBar.gameObject.SetActive(false);
    }
}
