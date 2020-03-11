using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //Instance
    public static GameManager Instance;

    //General
    [HideInInspector]
    public float SquaresAlive;
    public float Score;
    public float scoreMultiplier;
    private Coroutine currentTimer;
    private bool won;
    public GameObject Player;
    public float totalAmount;

    //Combo
    [SerializeField]
    private float comboTime, comboSoundAdd;
    [SerializeField]
    private bool comboActive;
    [SerializeField]
    private GameObject comboText;
    private Animator comboAnimator;
    [SerializeField]
    private AudioSource music;

    //Final Screen
    [SerializeField]
    GameObject finalScore;
    [SerializeField]
    public Text scoreText;
    private float currentScoreDisplay;

    //Achievements
    public int killCount;
    [HideInInspector]
    public AchievementManager achievementManager; 
    [HideInInspector]
    public GameObject AchievementPopUp;
    [HideInInspector]
    public string difficulty;
    public int MaxCombo;

    //PauseMenu
    [SerializeField]
    private GameObject pauseMenu;
    public bool isPaused;

    private void Awake()
    {
        Instance =  this;
        SquaresAlive = 0;
        Score = 0;
        killCount = 0;
        comboAnimator = comboText.GetComponent<Animator>();

        scoreMultiplier = 1;
        comboActive = false;
        StartCoroutine(WinChecker());
    }

    private void Start()
    {
        achievementManager = GameObject.FindGameObjectWithTag("AchievementManager").GetComponent<AchievementManager>();
        achievementManager.gameManager = this;
        achievementManager.StartCoroutine(achievementManager.CheckKillAmount());
        achievementManager.StartCoroutine(achievementManager.CheckComboAchievements());
        GameObject.FindGameObjectWithTag("ShipInitializer").GetComponent<ShipInitializer>().ChangeMaterials();

    }

    public void OnEndGame()
    {
        achievementManager.achievementData.deaths++;
        achievementManager.StopCoroutine(achievementManager.CheckKillAmount());
        achievementManager.StartCoroutine(achievementManager.CheckKillAmount());
        achievementManager.StopCoroutine(achievementManager.CheckComboAchievements());
        achievementManager.StartCoroutine(achievementManager.CheckComboAchievements());
        achievementManager.CheckScoreAchievements();
        achievementManager.CheckDeathAchievements();
        achievementManager.SaveAchievementData();
    }

    private IEnumerator WinChecker()
    {
        yield return new WaitForSeconds(0.05f);
        if (SquaresAlive <= 0 && !won)
        {
            won = true;
            Win();
        }
        else
        {
            StartCoroutine(WinChecker());
        }
    }

    private void Win()
    {
        achievementManager.CheckDifficultyAchievements(difficulty);
        StartCoroutine(lerpScore(0, Score, 4));
    }

    public void AddScore()
    {
        killCount++;
        if (comboActive)
        {
            scoreMultiplier++;
            if(scoreMultiplier > MaxCombo)
            {
                MaxCombo = (int)scoreMultiplier;
            }
        }
        Score += 10 * scoreMultiplier;

        if(currentTimer != null)
        {
            StopCoroutine(currentTimer);
        }
        currentTimer = StartCoroutine(ComboTimer());
    }

    private IEnumerator ComboTimer()
    {
        comboActive = true;
        if(scoreMultiplier >= 2)
        {
            if(music.volume < 1)
            {
                music.volume += comboSoundAdd;
            }
            comboAnimator.SetTrigger("Combo");
            comboText.SetActive(true);
        }
        yield return new WaitForSeconds(comboTime);

        StartCoroutine(lerpBackSound(music.volume, 0.5f, 0.5f));
        comboActive = false;
        comboText.SetActive(false);

        scoreMultiplier = 1;
    }

    public IEnumerator lerpBackSound(float startVolume, float endVolume, float LerpTime)
    {
        float StartTime = Time.time;
        float EndTime = StartTime + LerpTime;

        while (Time.time < EndTime)
        {
            float timeProgressed = (Time.time - StartTime) / LerpTime;  // this will be 0 at the beginning and 1 at the end.
            music.volume = Mathf.Lerp(startVolume, endVolume, timeProgressed);

            yield return new WaitForFixedUpdate();
        }
        music.volume = endVolume;
    }

    public IEnumerator lerpScore(float startNumber, float endNumber, float LerpTime)
    {
        finalScore.SetActive(true);
        float higscore = PlayerPrefs.GetFloat("Highscore");

        float StartTime = Time.time;
        float EndTime = StartTime + LerpTime;

        while (Time.time < EndTime)
        {
            float timeProgressed = (Time.time - StartTime) / LerpTime;  // this will be 0 at the beginning and 1 at the end.
            currentScoreDisplay = Mathf.Lerp(startNumber, endNumber, timeProgressed);
            scoreText.text = "Final Score:\n" + Mathf.RoundToInt(currentScoreDisplay) + "\n\n Higschore:\n" + higscore;
            yield return new WaitForFixedUpdate();
        }
        currentScoreDisplay = endNumber;
        scoreText.text = "Final Score:\n" + Mathf.RoundToInt(currentScoreDisplay) + "\n\n Higschore:\n" + higscore;

        CheckHighscore();
    }

    private void CheckHighscore()
    {
        if(Score > PlayerPrefs.GetFloat("Highscore"))
        {
            PlayerPrefs.SetFloat("Highscore", Score);
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(Time.timeScale == 1)
            {
                PauseGame();
            }
            else
            {
                UnPauseGame();
            }
        }
    }

    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        isPaused = true;
        Time.timeScale = 0;
    }

    public void UnPauseGame()
    {
        pauseMenu.SetActive(false);
        isPaused = false;
        Time.timeScale = 1;
    }
}
