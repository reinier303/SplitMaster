using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerDeath : MonoBehaviour
{
    GameManager gameManager;

    private void Start()
    {
        gameManager = GameManager.Instance;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Square")
        {
            Die();
        }
    }

    private void Die()
    {
        gameManager.StartCoroutine(gameManager.lerpScore(0, gameManager.Score, 3f));
        gameManager.OnEndGame();
        gameObject.SetActive(false);
    }

    public void Retry()
    {
        SceneManager.LoadSceneAsync(1);
    }
}
