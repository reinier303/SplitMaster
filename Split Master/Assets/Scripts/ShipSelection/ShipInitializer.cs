using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
public class ShipInitializer : MonoBehaviour
{
    private ScriptableShip ship;

    // Start is called before the first frame update
    private void Start()
    {
        //FIX BY PUTTING IN SHIP NAME FROM SHIPDATABASE
        ship = GameObject.FindGameObjectWithTag("ShipDataBase").GetComponent<ShipDataBase>().Ships["asdasd"];
        SetMaterials();
    }

    private void SetMaterials()
    {
        SpriteRenderer shipRenderer = GetComponent<SpriteRenderer>();
        SpriteRenderer indicatorRenderer = transform.GetChild(1).GetComponent<SpriteRenderer>();

        shipRenderer.material = ship.ShipMaterial;
        indicatorRenderer.material = ship.IndicatorMaterial;

        shipRenderer.sprite = ship.ShipSprite;
        indicatorRenderer.sprite = ship.IndicatorSprite;
    }
}


