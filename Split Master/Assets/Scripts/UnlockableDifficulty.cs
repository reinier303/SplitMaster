using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class UnlockableDifficulty : MonoBehaviour
{
    public ScriptableAchievement scriptableAchievement;
    private UIButtonR button;
    private Text text;
    public Text unlockText;

    public void Start()
    {
        AchievementManager achievementManager = AchievementManager.Instance;

        button = GetComponent<UIButtonR>();
        text = GetComponent<Text>();

        if(achievementManager.achievementData.AchievementUnlockStatus[scriptableAchievement.AchievementName])
        {
            if(scriptableAchievement.AchievementName == "Legendary Split Master")
            {
                transform.parent.gameObject.SetActive(true);
            }
            text.color = new Color(1,1,1, 1);
        }
        else
        {
            if (scriptableAchievement.AchievementName == "Legendary Split Master")
            {
                transform.parent.gameObject.SetActive(false);
            }
            else
            {
                button.OnPointerDownEvent = null;
                button.DownMaterial = button.BaseMaterial;

                text.color = new Color(0, 0, 0, 0.1f);
            }
        }
    }

    public void ShowUnlockText()
    {
        unlockText.gameObject.SetActive(true);
        string difficulty = scriptableAchievement.name.Replace("Done", "");
        unlockText.text = "You need to beat " + difficulty + " to unlock this difficulty";
    }

}
