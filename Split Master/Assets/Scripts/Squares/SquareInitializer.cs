using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SquareInitializer : MonoBehaviour
{
    [SerializeField]
    private ChangeCursor changeCursor;

    [SerializeField]
    private List<GameObject> Squares = new List<GameObject>();

    private int squareAmount;
    private int splitAmount;
    private int splitCount;

    [SerializeField]
    private GameObject Tutorial;

    private void Awake()
    {
        LevelData data = LoadDifficulty();
        squareAmount = data.GetAmount();
        splitAmount = data.GetSplitAmount();
        splitCount = data.GetSplitCount();

        GameManager gameManager = GameManager.Instance;

        gameManager.SquaresAlive = squareAmount;
        gameManager.difficulty = data.GetName();
        gameManager.totalAmount = squareAmount * Mathf.Pow(splitAmount, splitCount);

        Debug.Log(gameManager.totalAmount);
        if (data.GetTutorial())
        {
            EnableTutorial();
        }
        else
        {
            Initialze();
        }
    }

    // Start is called before the first frame update
    private void Initialze()
    {
        InitCubes();
        SpawnCubes();
        changeCursor.CursorCrosshair();
    }

    private void EnableTutorial()
    {
        Tutorial.SetActive(true);
        Time.timeScale = 0;
    }

    public void ExitTutorial()
    {
        Tutorial.SetActive(false);
        Time.timeScale = 1;
        Initialze();
    }

    private void SpawnCubes()
    {
        for (int i = 0; i < squareAmount; i++)
        {
            Squares[i].SetActive(true);
        }
    }

    private void InitCubes()
    {
        for(int i = 0; i < squareAmount; i++)
        {
            Squares[i].GetComponent<Square>().SplitAmount = splitAmount;
        }
        for (int i = 0; i < squareAmount; i++)
        {
            Squares[i].GetComponent<Square>().SplitCount = splitCount;
        }
    }

    private LevelData LoadDifficulty()
    {
        string path = Application.persistentDataPath + "/LevelData.init";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            LevelData data = formatter.Deserialize(stream) as LevelData;
            stream.Close();

            return data;
        }
        else
        {
            Debug.Log("LevelData not found " + path);
            return null;
        }
    }
}
