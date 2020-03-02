using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class MenuParallax : MonoBehaviour
{
    private float length, startPos;
    [SerializeField]
    private GameObject cam;
    [SerializeField]
    private float parallaxDistance;
    [SerializeField]
    private float speed;

    // Start is called before the first frame update
    void Start()
    {
        startPos = (cam.transform.position.x * parallaxDistance);
    }

    // Update is called once per frame
    void Update()
    {
        float distX = (cam.transform.position.x * parallaxDistance);
        float distY = (cam.transform.position.y * parallaxDistance);

        Vector2 newPosition = new Vector2(startPos + distX, startPos + distY);

        transform.position = newPosition ;

    }
}
