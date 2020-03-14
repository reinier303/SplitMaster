using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMouse : MonoBehaviour
{
    private Camera camera;

    private void Start()
    {
        camera = Camera.main;

    }
    // Update is called once per frame
    void Update()
    {
        transform.position = (Vector2)camera.ScreenToWorldPoint(Input.mousePosition);
    }
}
