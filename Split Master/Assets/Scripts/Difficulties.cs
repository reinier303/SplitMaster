using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Difficulties : MonoBehaviour
{
    public static Difficulties Instance;

    public List<LevelData> DifficultiesList = new List<LevelData>();

    public LevelData currentDifficulty;

    private void Awake()
    {
        //Destroy if already existing.
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Difficulties");

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);

        Instance = this;
    }

    public void NextDifficulty()
    {
        DifficultiesList = DifficultiesList.OrderBy(x => x.difficultyOrder).ToList();

        currentDifficulty = DifficultiesList[currentDifficulty.difficultyOrder + 1];

        MenuButton menuButton = gameObject.AddComponent<MenuButton>();

        menuButton.difficultyName = currentDifficulty.difficultyName;
        menuButton.order = currentDifficulty.difficultyOrder;
        menuButton.splitAmount = currentDifficulty.splitAmount;
        menuButton.splitCount = currentDifficulty.splitCount;
        menuButton.squareAmount = currentDifficulty.squareAmount;

        menuButton.GoToDifficulty();
    }
}
