using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementPanelInitializer : MonoBehaviour
{
    private AchievementManager achievementManager;
    public GameObject AchievementSlot;

    private void Start()
    {
        achievementManager = GameObject.FindGameObjectWithTag("AchievementManager").GetComponent<AchievementManager>();
        foreach(ScriptableAchievement achievement in achievementManager.Achievements)
        {
            AchievementSlot achievementSlot = Instantiate(AchievementSlot, transform).GetComponent<AchievementSlot>();
            achievementSlot.achievement = achievement;
            achievementSlot.achievementManager = achievementManager;
            achievementSlot.Initialize();
        }
    }
}
