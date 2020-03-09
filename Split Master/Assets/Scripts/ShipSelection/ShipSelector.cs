using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class ShipSelector : MonoBehaviour
{
    private RectTransform rect;
    [SerializeField]
    private float lerpTime , disappearTime;
    private bool running;
    private List<ScriptableShip> ships = new List<ScriptableShip>();
    [SerializeField]
    private int currentShip;
    [SerializeField]
    private GameObject leftButton, rightButton;
    [SerializeField]
    private GameObject MenuShipPrefab;
    public Text UnlockText;
    public GameObject SetShipButton;
    private ShipInitializer shipInitializer;
    private AchievementManager achievementManager;

    private void Awake()
    {
        //Get Ships from Resources folder
        Object[] scriptableShips = Resources.LoadAll("Ships", typeof(ScriptableShip));
        foreach (ScriptableShip ship in scriptableShips)
        {
            if (ships.Contains(ship))
            {
                continue;
            }
            //Check if pool info is filled.
            if (ship.ShipName != null && ship.ShipSprite != null && ship.IndicatorSprite != null && ship.ShipMaterial != null && ship.IndicatorMaterial != null)
            {
                ships.Add(ship);
            }
            else
            {
                Debug.LogWarning("Achievement: " + ship.ShipName + " is missing some information. \n Please go back to Resources/Ships and fill in the information correctly");
            }
        }

        ships = ships.OrderBy(x => x.IndexNumber).ToList();

        //Put ships in menu
        int shipNumber = 0;

        foreach (ScriptableShip ship in ships)
        {
            Vector2 position = new Vector2(480 * shipNumber, 0);
            GameObject shipObject = Instantiate(MenuShipPrefab, transform);
            shipObject.transform.localPosition = position;
            MenuShip menuShipScript = shipObject.GetComponent<MenuShip>();
            menuShipScript.ship = ship;
            menuShipScript.Initialize();
            shipNumber++;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        shipInitializer = GameObject.FindGameObjectWithTag("ShipInitializer").GetComponent<ShipInitializer>();
        achievementManager = GameObject.FindGameObjectWithTag("AchievementManager").GetComponent<AchievementManager>();

        currentShip = PlayerPrefs.GetInt("ShipIndex");
        SetShip();
        rect = GetComponent<RectTransform>();
        running = false;

        rect.anchoredPosition = new Vector2((currentShip * -480), rect.anchoredPosition.y);

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
            StartCoroutine(lerpPosition(rect.anchoredPosition, newPos, lerpTime));
        }
        ScriptableAchievement shipAchievement = ships[currentShip].Achievement;
        if (shipAchievement != null && !achievementManager.achievementData.AchievementUnlockStatus[shipAchievement.AchievementName])
        {
            UnlockText.gameObject.SetActive(true);
            SetShipButton.SetActive(false);
            UnlockText.text = "You must achieve: " + ships[currentShip].Achievement.AchievementName + " to unlock this skin";
        }
        else
        {
            UnlockText.gameObject.SetActive(false);
            SetShipButton.SetActive(true);
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

    public void SetShip()
    {
        PlayerPrefs.SetInt("ShipIndex", currentShip);
        shipInitializer.SetShip(ships[currentShip]);
    }
}
