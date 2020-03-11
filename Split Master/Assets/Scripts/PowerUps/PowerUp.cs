using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PowerUp : MonoBehaviour
{
    protected PlayerShootSingle playerShootScript;

    public float duration;

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        playerShootScript = other.GetComponent<PlayerShootSingle>();
        if(playerShootScript != null)
        {
            ActivatePowerUp();
            gameObject.SetActive(false);
        }
    }

    protected virtual void ActivatePowerUp()
    {
        //This method is meant to be overridden.
    }

}
