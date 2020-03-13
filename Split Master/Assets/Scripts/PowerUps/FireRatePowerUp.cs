using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireRatePowerUp : PowerUp
{
    public float FireRateMultiplier;

    protected override void ActivatePowerUp()
    {
        if (playerShootScript.runningCoroutine != null)
        {
            playerShootScript.StopCoroutine(playerShootScript.runningCoroutine);
            playerShootScript.runningCoroutine = null;
        }
        playerShootScript.runningCoroutine = playerShootScript.StartCoroutine(playerShootScript.FireRateUp(duration, FireRateMultiplier));
    }
}
