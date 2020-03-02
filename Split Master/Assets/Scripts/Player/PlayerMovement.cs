using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float speed;
    private Vector3 target;
    private Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main; ;
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
    void Update()
    {
#if UNITY_ANDROID
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
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        if (horizontal != 0 || vertical != 0)
        {
                transform.position += new Vector3(horizontal, vertical).normalized * Time.deltaTime * speed;
        }
    }

    private void Rotate()
    {
        var dir = target - transform.position;
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
    }

    #endregion

    #region MobileMovement

    private void MoveMobile()
    {
        float horizontal = CrossPlatformInputManager.GetAxis("Horizontal");
        float vertical = CrossPlatformInputManager.GetAxis("Vertical");

        if (horizontal != 0 || vertical != 0)
        {
            transform.position += new Vector3(horizontal, vertical).normalized * Time.deltaTime * speed;
        }
    }

    private void RotateMobile()
    {
        float horizontal = CrossPlatformInputManager.GetAxis("HorizontalR");
        float vertical = CrossPlatformInputManager.GetAxis("VerticalR");
        if(horizontal != 0 || vertical != 0)
        {
            Vector3 lookVec = new Vector3(horizontal, vertical, 4096);
            Quaternion targetRotation = Quaternion.LookRotation(lookVec, Vector3.back);
            transform.rotation = targetRotation;
        }
    }

    #endregion
}
