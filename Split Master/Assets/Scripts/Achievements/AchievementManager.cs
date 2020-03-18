using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Linq;

public class AchievementManager : MonoBehaviour
{
    //Instance
    public static AchievementManager Instance;

    //All Achievements
    [HideInInspector]
    public List<ScriptableAchievement> Achievements = new List<ScriptableAchievement>();

    //Achievement Groups
    private List<ScriptableAchievement> KillAmountAchievements = new List<ScriptableAchievement>();
    private List<ScriptableAchievement> ScoreAchievements = new List<ScriptableAchievement>();
    private List<ScriptableAchievement> DeathAmountAchievements = new List<ScriptableAchievement>();
    private List<ScriptableAchievement> DifficultyAchievements = new List<ScriptableAchievement>();
    private List<ScriptableAchievement> ComboAchievements = new List<ScriptableAchievement>();

    //Achievement Data
    public AchievementData achievementData;

    //Script References
    public GameManager gameManager;

    private GameObject achievementPopUp;

    private Dictionary<string, bool> AchievementUnlockStatus = new Dictionary<string, bool>();

    private List<ScriptableAchievement> achievementQueue = new List<ScriptableAchievement>();

    private bool popUpShowing;

    private void Awake()
    {
        //Destroy if already existing.
        GameObject[] objs = GameObject.FindGameObjectsWithTag("AchievementManager");

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);

        Instance = this;

        InitializeAchievements();
        SceneManager.activeSceneChanged += InitializeOnSceneChange;

    }

    private void InitializeAchievements()
    {
        //Load Achievement data from file
        achievementData = SaveLoad.Load<AchievementData>("AchievementData");

        //Get all achievements
        Object[] ScriptableAchievements = Resources.LoadAll("Achievements", typeof(ScriptableAchievement));
        foreach (ScriptableAchievement achievement in ScriptableAchievements)
        {
            if (Achievements.Contains(achievement))
            {
                continue;
            }
            //Check if pool info is filled.
            if (achievement.Amount > 0 && achievement.AchievementName != null && achievement.AchievementGroup != null && achievement.Description != null)
            {
                Achievements.Add(achievement);
                if (achievementData == null)
                {
                    AchievementUnlockStatus.Add(achievement.AchievementName, false);
                }
                else
                {
                    AchievementUnlockStatus = achievementData.AchievementUnlockStatus;
                }
            }
            else
            {
                Debug.LogWarning("Achievement: " + achievement.AchievementName + " is missing some information. \n Please go back to Resources/Achievements and fill in the information correctly");
            }
        }

        Achievements = Achievements.OrderBy(x => x.Index).ToList();
    }

    private void InitializeOnSceneChange(Scene current, Scene next)
    {
        gameManager = GameManager.Instance;

        //Load Achievement data from file
        achievementData = SaveLoad.Load<AchievementData>("AchievementData");

        //Get All Achievements
        InitializeAchievements();

        //Get groups
        foreach (ScriptableAchievement achievement in Achievements)
        {
            if(achievement.AchievementGroup == "KillAmount")
            {
                KillAmountAchievements.Add(achievement);
                continue;
            }
            if (achievement.AchievementGroup == "DeathAmount")
            {
                DeathAmountAchievements.Add(achievement);
                continue;
            }
            if (achievement.AchievementGroup == "Score")
            {
                ScoreAchievements.Add(achievement);
                continue;
            }
            if (achievement.AchievementGroup == "Difficulty")
            {
                DifficultyAchievements.Add(achievement);
                continue;
            }
            if (achievement.AchievementGroup == "Combo")
            {
                ComboAchievements.Add(achievement);
                continue;
            }
        }

        //Create new save if no save is found
        if (achievementData == null)
        {
            print("New data made");
            achievementData = new AchievementData();
            SaveAchievementData();
        }
    }

    public IEnumerator CheckKillAmount()
    {
        achievementData.kills += gameManager.killCount;
        gameManager.killCount = 0;
        foreach(ScriptableAchievement achievement in KillAmountAchievements)
        {
            if (achievementData.kills >= achievement.Amount && !AchievementUnlockStatus[achievement.AchievementName])
            {
                AchievementUnlockStatus[achievement.AchievementName] = true;
                StartCoroutine(ShowPopUp(achievement));
            }
        }
        yield return new WaitForSeconds(5f);
        StartCoroutine(CheckKillAmount());
    }

    public void CheckScoreAchievements()
    {
        foreach (ScriptableAchievement achievement in ScoreAchievements)
        {
            if (gameManager.Score >= achievement.Amount && !AchievementUnlockStatus[achievement.AchievementName])
            {
                AchievementUnlockStatus[achievement.AchievementName] = true;
                StartCoroutine(ShowPopUp(achievement));
            }
        }
    }

    public IEnumerator CheckComboAchievements()
    {
        foreach (ScriptableAchievement achievement in ComboAchievements)
        {
            if (gameManager.MaxCombo >= achievement.Amount && !AchievementUnlockStatus[achievement.AchievementName])
            {
                AchievementUnlockStatus[achievement.AchievementName] = true;
                StartCoroutine(ShowPopUp(achievement));
            }
        }
        yield return new WaitForSeconds(5f);
        StartCoroutine(CheckComboAchievements());
    }

    public void CheckDeathAchievements()
    {
        foreach (ScriptableAchievement achievement in DeathAmountAchievements)
        {
            if (achievementData.deaths >= achievement.Amount && !AchievementUnlockStatus[achievement.AchievementName])
            {
                AchievementUnlockStatus[achievement.AchievementName] = true;
                StartCoroutine(ShowPopUp(achievement));
            }
        }
    }

    public void CheckDifficultyAchievements(string difficulty)
    {
        switch (difficulty)
        {
            case "Tutorial":
                if(!AchievementUnlockStatus["Initiate"])
                {
                    AchievementUnlockStatus["Initiate"] = true;
                    StartCoroutine(ShowPopUp(Achievements[26]));
                }
                break;
            case "Easy":
                if (!AchievementUnlockStatus["Splitter"])
                {
                    AchievementUnlockStatus["Splitter"] = true;
                    StartCoroutine(ShowPopUp(Achievements[27]));
                }
                break;
            case "Medium":
                if (!AchievementUnlockStatus["Split Master"])
                {
                    AchievementUnlockStatus["Split Master"] = true;
                    StartCoroutine(ShowPopUp(Achievements[28]));
                }
                break;
            case "Hard":
                if (!AchievementUnlockStatus["Legendary Split Master"])
                {
                    AchievementUnlockStatus["Legendary Split Master"] = true;
                    StartCoroutine(ShowPopUp(Achievements[29]));
                }
                break;
            case "Extreme":
                if (!AchievementUnlockStatus["Legendary Split Grandmaster"])
                {
                    AchievementUnlockStatus["Legendary Split Grandmaster"] = true;
                    StartCoroutine(ShowPopUp(DifficultyAchievements[30]));
                }
                break;
            default:
                return;

        }
        SaveAchievementData();
    }

    public void SaveAchievementData()
    {
        achievementData.AchievementUnlockStatus = AchievementUnlockStatus;
        SaveLoad.Save<AchievementData>(achievementData, "AchievementData");
    }

    private IEnumerator ShowPopUp(ScriptableAchievement achievement)
    {
        if(popUpShowing)
        {
            achievementQueue.Add(achievement);
            yield break;
        }
        popUpShowing = true;

        achievementPopUp = gameManager.AchievementPopUp;
        achievementPopUp.SetActive(true);
        achievementPopUp.transform.GetChild(0).GetComponent<Text>().text = achievement.AchievementName;
        achievementPopUp.transform.GetChild(1).GetComponent<Text>().text = achievement.Description;

        yield return new WaitForSeconds(5f);
        achievementPopUp.GetComponent<Animator>().SetTrigger("PopOut");
        yield return new WaitForSeconds(1f);

        achievementPopUp.SetActive(false);
        popUpShowing = false;
        if(achievementQueue.Count > 0)
        {
            StartCoroutine(ShowPopUp(achievementQueue[0]));
            achievementQueue.RemoveAt(0);
        }
    }
}

[System.Serializable]
public class AchievementData
{
    public float kills = 0;
    public float deaths = 0;
    public Dictionary<string, bool> AchievementUnlockStatus;
}

