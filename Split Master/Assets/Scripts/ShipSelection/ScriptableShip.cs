using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Ship", order = 2)]
public class ScriptableShip : ScriptableObject
{
    public string ShipName;
    public Color ShipColor;
    public Color IndicatorColor;
    public Sprite ShipSprite;
    public Sprite IndicatorSprite;
    public Material ShipMaterial;
    public Material IndicatorMaterial;
    public ScriptableAchievement Achievement = null;
}
