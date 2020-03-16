using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Achievement", order = 1)]
public class ScriptableAchievement : ScriptableObject
{
    public float Index;
    public string AchievementGroup;
    public string AchievementName;
    public bool Unlocked;
    public int Amount;
    [TextArea(5,5)]
    public string Description;
    public Sprite icon;
}