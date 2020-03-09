using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class AchievementSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private GameObject infoPanel;
    private Text achievementTitle, achievementDescription;
    public ScriptableAchievement achievement;
    public Image Icon;
    private Image background;
    [HideInInspector]
    public AchievementManager achievementManager;
    private AudioSource audioSource;


    public void Initialize()
    {
        infoPanel = transform.parent.parent.GetChild(2).gameObject;
        Icon.sprite = achievement.icon;
        achievementTitle = infoPanel.transform.GetChild(0).GetComponent<Text>();
        achievementDescription = infoPanel.transform.GetChild(1).GetComponent<Text>();
        background = GetComponent<Image>();
        audioSource = GetComponent<AudioSource>();
        CheckUnlock();
    }

    private void CheckUnlock()
    {
        if(achievementManager.achievementData.AchievementUnlockStatus.ContainsKey(achievement.AchievementName))
        {
            if (achievementManager.achievementData.AchievementUnlockStatus[achievement.AchievementName] == false)
            {
                LockAchievement();
            }
        }
        else
        {
            Debug.LogWarning("Achievement with name: " + achievement.AchievementName + " has not been found");
        }
    }

    private void LockAchievement()
    {
        background.color = new Color(background.color.r, background.color.g, background.color.b, 0);
        Icon.color = new Color(Icon.color.r, Icon.color.g, Icon.color.b, 0.5f);
    }

    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        achievementTitle.text = achievement.AchievementName;
        achievementDescription.text = achievement.Description;
        audioSource.Play();
        infoPanel.SetActive(true);
    }

    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        infoPanel.SetActive(false);
    }
}
