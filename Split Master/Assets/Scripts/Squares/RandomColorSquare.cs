using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomColorSquare : MonoBehaviour
{
    SpriteRenderer spriteRenderer;

    ParticleSystem particleSystem;

    // Start is called before the first frame update
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        particleSystem = transform.GetChild(0).GetComponent<ParticleSystem>(); ;

        RandomColor();
    }

    private void RandomColor()
    {
        //Generate random color, but not too dark.
        int r = Random.Range(63, 256);
        int g = Random.Range(63, 256);
        int b = Random.Range(63, 256);

        int random3 = Random.Range(1, 4);
        switch (random3)
        {
            case 1:
                r = 63;
                break;
            case 2:
                g = 63;
                break;
            case 3:
                b = 63;
                break;
        }

        Vector4 newColor = new Vector4(r, g, b, 255);
        Color32 newColor2 = new Color32((byte)r, (byte)g, (byte)b, 255);
         
        //Pass color to square script for further use.
        Square square = GetComponent<Square>();
        square.newColor = newColor;
        square.newColor2 = newColor2;

        //Set random color on sprite renderer.
        spriteRenderer.color = newColor;
        spriteRenderer.material.SetColor("_Color", newColor2);
        spriteRenderer.material.SetVector("_EmissionColor", newColor * 0.017f);

        //Set random color on particle effect.
        Material particleSystemMaterial = particleSystem.GetComponent<ParticleSystemRenderer>().material;
        particleSystemMaterial.SetColor("_Color", newColor2);
        particleSystemMaterial.SetVector("_EmissionColor", newColor * 0.017f);

    }
}
