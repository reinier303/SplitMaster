using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCursor : MonoBehaviour
{
    Camera camera;
    public Transform CursorTransform;

    private void Start()
    {
        camera = GetComponent<Camera>();
        Cursor.visible = false;
    }

    private void FixedUpdate()
    {
        Vector3 mousePos = camera.ScreenToWorldPoint(Input.mousePosition);
        CursorTransform.transform.position = new Vector3(mousePos.x, mousePos.y, 10);
    }
}
