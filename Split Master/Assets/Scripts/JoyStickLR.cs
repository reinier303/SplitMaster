using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoyStickLR : MonoBehaviour
{
    public bool Left;

    // Start is called before the first frame update
    void Awake()
    {
        GetComponent<Joystick>().left = Left;
    }
}
