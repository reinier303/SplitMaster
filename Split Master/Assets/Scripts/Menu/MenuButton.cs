using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.SceneManagement;

public class MenuButton : MonoBehaviour
{
    private Difficulties difficulties;

    public int order;
    public string difficultyName;
    public int squareAmount;
    public int splitAmount;
    public int splitCount;
    public bool tutorial;

    public void Start()
    {
        difficulties = Difficulties.Instance;
        LevelData difficulty = new LevelData(difficultyName, squareAmount, splitAmount, splitCount, tutorial, order);
        difficulties.DifficultiesList.Add(difficulty);
    }

    public void GoToDifficulty()
    {
        difficulties = Difficulties.Instance;
        difficulties.currentDifficulty = new LevelData(difficultyName, squareAmount, splitAmount, splitCount, tutorial, order);
        SaveDifficulty();
        if(SceneManager.GetActiveScene().buildIndex == 0)
        {
            GetComponentInParent<Menu>().LoadScene(1);
        }
        else
        {
            SceneManager.LoadSceneAsync(1);
            Destroy(this);
        }
    }

    private void SaveDifficulty()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/LevelData.init";

        FileStream stream = new FileStream(path, FileMode.Create);

        LevelData data = new LevelData(difficultyName, squareAmount, splitAmount, splitCount, tutorial, order);

        formatter.Serialize(stream, data);
        stream.Close();
    }
}

[System.Serializable]
public class LevelData
{
    public int difficultyOrder;
    public string difficultyName;
    public int squareAmount;
    public int splitAmount;
    public int splitCount;
    public bool tutorial;

    public LevelData(string newDifficultyName, int newSquareAmount, int newSplitAmount, int newSplitCount, bool isTutorial, int newDifficultyOrder)
    {
        difficultyName = newDifficultyName;
        squareAmount = newSquareAmount;
        splitAmount = newSplitAmount;
        splitCount = newSplitCount;
        tutorial = isTutorial;
        difficultyOrder = newDifficultyOrder;
    }

    public string GetName()
    {
        return difficultyName;
    }
    public int GetAmount()
    {
        return squareAmount;
    }
    public int GetSplitAmount()
    {
        return splitAmount;
    }
    public int GetSplitCount()
    {
        return splitCount;
    }
    public bool GetTutorial()
    {
        return tutorial;
    }
}
