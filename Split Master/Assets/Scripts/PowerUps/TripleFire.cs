using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TripleFire : PowerUp
{
    public float spread;

    protected override void ActivatePowerUp()
    {
        playerShootScript.StartCoroutine(playerShootScript.TripleFire(duration, spread));
    }
}
