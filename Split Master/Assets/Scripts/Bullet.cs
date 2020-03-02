using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private float speed;

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.up * speed * Time.deltaTime;
        RemoveBulletAtBounds();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Square")
        {
            collision.GetComponent<Square>().Die();
            gameObject.SetActive(false);
        }
    }

    private void RemoveBulletAtBounds()
    {
        if (transform.position.x > 25 || transform.position.y > 25 || transform.position.x < -25 || transform.position.y < -25)
        {
            gameObject.SetActive(false);
        }
    }
}
