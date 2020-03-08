using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
public class ShipInitializer : MonoBehaviour
{
    private ScriptableShip ship;
    private GameObject Player;

    private void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("ShipInitializer");

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }

    public void SetShip(ScriptableShip newShip)
    {
        ship = newShip;
    }

    // Start is called before the first frame update
    public void ChangeMaterials()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        if(Player != null)
        {
            SetMaterials();
        }
    }

    private void SetMaterials()
    {
        SpriteRenderer shipRenderer = Player.GetComponent<SpriteRenderer>();
        SpriteRenderer indicatorRenderer = Player.transform.GetChild(1).GetComponent<SpriteRenderer>();

        shipRenderer.sprite = ship.ShipSprite;
        shipRenderer.color = ship.ShipColor;
        shipRenderer.material = ship.ShipMaterial;

        indicatorRenderer.sprite = ship.IndicatorSprite;
        indicatorRenderer.color = ship.IndicatorColor;
        indicatorRenderer.material = ship.IndicatorMaterial;
    }
}


