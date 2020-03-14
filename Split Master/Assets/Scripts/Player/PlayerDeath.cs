using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerDeath : MonoBehaviour
{
    private GameManager gameManager;
    public GameObject DeathEffect;

    private void Start()
    {
        gameManager = GameManager.Instance;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Square")
        {
            gameManager.StartCoroutine(Die());
        }
    }

    private IEnumerator Die()
    {
        DeathEffect.SetActive(true);
        DeathEffect.transform.SetParent(transform.parent);
        gameObject.SetActive(false);

        yield return new WaitForSeconds(3f);

        gameManager.StartCoroutine(gameManager.lerpScore(0, gameManager.Score, 3f));
        gameManager.OnEndGame();

    }

    public void Retry()
    {
        SceneManager.LoadSceneAsync(1);
    }
}
