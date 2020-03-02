using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ParallaxCamera : MonoBehaviour
{
    public delegate void ParallaxCameraDelegate(Vector2 deltaMovement);
    public ParallaxCameraDelegate onCameraTranslate;
    private float oldPositionX;
    private float oldPositionY;

    void Start()
    {
        oldPositionX = transform.position.x;
        oldPositionY = transform.position.y;
    }
    void FixedUpdate()
    {
        if (transform.position.x != oldPositionX || transform.position.y != oldPositionY)
        {
            if (onCameraTranslate != null)
            {
                float deltaX = oldPositionX - transform.position.x;
                float deltaY = oldPositionY - transform.position.y;

                onCameraTranslate(new Vector2(deltaX,deltaY));
            }
            oldPositionX = transform.position.x;
            oldPositionY = transform.position.y;
        }
    }
}