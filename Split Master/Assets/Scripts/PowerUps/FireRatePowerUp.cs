using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireRatePowerUp : PowerUp
{
    public float FireRateMultiplier;

    protected override void ActivatePowerUp()
    {
        playerShootScript.StartCoroutine(playerShootScript.FireRateUp(duration, FireRateMultiplier));
    }
}
