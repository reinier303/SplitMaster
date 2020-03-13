using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBlast : PowerUp
{
    private ObjectPooler objectPooler;
    private GameManager gameManager;

    public int bulletCount;
    public float PushDistance;
    public float PushSpeed;

    private GameObject Player;

    protected void Awake()
    {
        objectPooler = InstanceManager<ObjectPooler>.GetInstance("ObjectPooler");
        gameManager = GameManager.Instance;
        Player = gameManager.Player;
    }
    protected override void OnTriggerEnter2D(Collider2D other)
    {
        Bullet bullet = other.GetComponent<Bullet>();
        if(bullet != null)
        {
            ActivatePowerUp();
        }
    }

    protected override void ActivatePowerUp()
    {
        for(int i = 0; i < bulletCount; i ++)
        {
            objectPooler.SpawnFromPool("RoundBullet", transform.position, Quaternion.Euler(new Vector3(0, 0, (360 / bulletCount) * i)));
        }
        gameObject.SetActive(false);
    }

    public void Update()
    {
        float distance = Vector2.Distance(transform.position, Player.transform.position);
        if (distance <= PushDistance)
        {
            float x = Player.transform.position.x - transform.position.x;
            float y = Player.transform.position.y - transform.position.y;
            Vector3 newPosition = new Vector3(x, y, 0) * Time.deltaTime;
            transform.position -= (newPosition * PushSpeed) / distance;
        }
    }
}
