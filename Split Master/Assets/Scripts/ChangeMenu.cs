using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeMenu : MonoBehaviour
{
    RectTransform rect;
    RectTransform other;

    [SerializeField]
    private float lerpTime;
    private bool running;
    [SerializeField]
    private GameObject shipArrow, menuArrowUp, menuArrowDown, achievementArrow;

    private Button currentButton;

    private void Start()
    {
        rect = GetComponent<RectTransform>();
        if (rect.anchoredPosition.y == 0)
        {
            menuArrowUp.SetActive(false);
        }
        else if (rect.anchoredPosition.y == -1080)
        {
            shipArrow.SetActive(false);
        }
    }

    public void ChangePosition(float Amount)
    {
        Vector2 newPos = new Vector2(rect.anchoredPosition.x, rect.anchoredPosition.y + Amount);
        StartCoroutine(lerpPosition(rect.anchoredPosition, newPos, lerpTime));
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
        running = false;
    }
}
