using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerMovementSingle : MonoBehaviour
{
    [SerializeField]
    private float speed;
    private Vector3 target;
    private Camera cam;
    [SerializeField]
    private Joystick joyStick;

    private float horizontalL, verticalL;
    private float horizontalR, verticalR;


    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        horizontalL = 0;
        verticalL = 0;
    }

    private void OnGUI()
    {
        Event currentEvent = Event.current;
        Vector2 mousePos = new Vector2();

        // compute where the mouse is in world space
        mousePos.x = currentEvent.mousePosition.x;
        mousePos.y = cam.pixelHeight - currentEvent.mousePosition.y;
        target = cam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 0.0f));
    }

    // Update is called once per frame
    void FixedUpdate()
    {
#if UNITY_ANDROID
        GetAxises();
        MoveMobile();
        RotateMobile();
#endif
#if UNITY_STANDALONE_WIN || UNITY_EDITOR || UNITY_WEBGL
        Move();
        Rotate();
#endif
    }

    #region PCMovement

    private void Move()
    {

        float horizontalPc = Input.GetAxis("Horizontal");
        float verticalPc = Input.GetAxis("Vertical");

        float clamped = Mathf.Clamp01(Vector2.SqrMagnitude(new Vector2(horizontalPc, verticalPc)));
        transform.position += new Vector3(horizontalPc, verticalPc).normalized * clamped * Time.deltaTime * speed;

    }

    private void Rotate()
    {
        var dir = target - transform.position;
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
    }

    #endregion

    #region MobileMovement
    
    private void GetAxises()
    {
        horizontalL = joyStick.HorizontalL;
        verticalL = joyStick.VerticalL;
    }

    private void MoveMobile()
    {
        if (horizontalL != 0 || verticalL != 0)
        {
            transform.position += new Vector3(horizontalL, verticalL).normalized * Time.deltaTime * speed;
        }
    }

    private void RotateMobile()
    {
        if(horizontalL != 0 || verticalL != 0)
        {
            Vector3 lookVec = new Vector3(horizontalL, verticalL, 4096);
            Quaternion targetRotation = Quaternion.LookRotation(lookVec, Vector3.back);
            transform.rotation = targetRotation;
        }
    }

    #endregion
}
