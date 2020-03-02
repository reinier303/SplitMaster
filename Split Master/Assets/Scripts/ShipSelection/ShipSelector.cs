using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEditor;

public class ShipSelector : MonoBehaviour
{
    private RectTransform rect;
    [SerializeField]
    private float lerpTime , disappearTime;
    private bool running;
    private List<GameObject> ships = new List<GameObject>();
    [SerializeField]
    private int currentShip;
    [SerializeField]
    private GameObject leftButton, rightButton;
    [SerializeField]
    private ShipDataBase shipDataBase;

    // Start is called before the first frame update
    void Start()
    {
        rect = GetComponent<RectTransform>();
        running = false;

        if (rect.anchoredPosition.x == 0)
        {
            leftButton.SetActive(false);
        }
        else if (rect.anchoredPosition.x == (ships.Count - 1) * -480)
        {
            rightButton.SetActive(false);
        }
    }

    // Update is called once per frame
    public void ChangePosition(float Amount)
    {
        Vector2 newPos = new Vector2(rect.anchoredPosition.x + Amount, rect.anchoredPosition.y);
        if(!running)
        {
            if (Amount < 0)
            {
                currentShip++;
            }
            else
            {
                currentShip--;
            }
            PlayerPrefs.SetInt("ShipNumber", currentShip);
            StartCoroutine(lerpPosition(rect.anchoredPosition, newPos, lerpTime));
        }
    }

    public IEnumerator lerpPosition(Vector2 startPosition, Vector2 endPosition, float LerpTime)
    {
        running = true;
        float StartTime = Time.time;
        float EndTime = StartTime + LerpTime;

        while (Time.time < EndTime)
        {
            float timeProgressed = (Time.time - StartTime) / LerpTime;  // this will be 0 at the beginning and 1 at the end.
            rect.anchoredPosition = Vector2.Lerp(startPosition, endPosition, timeProgressed);

            yield return new WaitForFixedUpdate();
        }
        rect.anchoredPosition = endPosition;
        if(rect.anchoredPosition.x == 0)
        {
            leftButton.SetActive(false);
        }
        else if (rect.anchoredPosition.x == (ships.Count -1) * -480)
        {
            rightButton.SetActive(false);
        }
        else
        {
            leftButton.SetActive(true);
            rightButton.SetActive(true);
        }
        running = false;
    }
}
