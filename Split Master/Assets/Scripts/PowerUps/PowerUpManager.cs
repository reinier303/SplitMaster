using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpManager : MonoBehaviour
{
    public static PowerUpManager Instance;

    private List<string> powerUps = new List<string>();
    ObjectPooler objectPooler;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    private void Start()
    {
        objectPooler = InstanceManager<ObjectPooler>.GetInstance("ObjectPooler");

        powerUps.Add("BulletBlast");
        powerUps.Add("BulletBlast");
        powerUps.Add("TripleFire");
        powerUps.Add("FireRate");
    }

    public void SpawnPowerUp(float chance, Vector2 position)
    {
        float random = Random.Range(0, 100);
        if(random <= chance)
        {
            objectPooler.SpawnFromPool(powerUps[Random.Range(0, powerUps.Count)], position, Quaternion.identity);
        }
    }
}
