using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuShip : MonoBehaviour
{
    public ScriptableShip ship;
    [SerializeField]
    private Text nameText;
    [SerializeField]
    private SpriteRenderer shipRenderer, indicatorRenderer;
    private ShipSelector shipSelector;

    public void Initialize()
    {
        shipSelector = transform.GetComponentInParent<ShipSelector>();

        nameText.text = ship.ShipName;

        shipRenderer.sprite = ship.ShipSprite;
        shipRenderer.color = ship.ShipColor;
        shipRenderer.material = ship.ShipMaterial;

        indicatorRenderer.sprite = ship.IndicatorSprite;
        indicatorRenderer.color = ship.IndicatorColor;
        indicatorRenderer.material = ship.IndicatorMaterial;
    }
}
