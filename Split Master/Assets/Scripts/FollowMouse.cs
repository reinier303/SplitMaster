using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class FollowMouse : MonoBehaviour
{
    private Camera camera;
    public CinemachineCameraOffset cameraOffset;

    private void Start()
    {
        camera = Camera.main;

    }
    // Update is called once per frame
    void Update()
    {
        transform.localPosition = (Vector2)transform.parent.position - (Vector2)camera.ScreenToWorldPoint(Input.mousePosition);

        cameraOffset.m_Offset = Vector2.Lerp(cameraOffset.m_Offset, -transform.localPosition / 5, 0.2f);

    }
}
