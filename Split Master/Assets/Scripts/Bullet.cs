using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private float speed, aliveTime;

    private ObjectPooler objectPooler;

    private void Start()
    {
        objectPooler = InstanceManager<ObjectPooler>.GetInstance("ObjectPooler");
    }

    private void OnEnable()
    {
        StartCoroutine(DisableAfterTime());
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.up * speed * Time.deltaTime;
        RemoveBulletAtBounds();
    }

    private IEnumerator DisableAfterTime()
    {
        yield return new WaitForSeconds(aliveTime);
        gameObject.SetActive(false);
    }

    private void RemoveBulletAtBounds()
    {
        if (transform.position.x > 25 || transform.position.y > 25 || transform.position.x < -25 || transform.position.y < -25)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Square square = collision.GetComponent<Square>();
        if(square != null)
        {
            objectPooler.SpawnFromPool("BulletPop", transform.position, Quaternion.identity);
            square.Die();
            gameObject.SetActive(false);
        }
    }
}
