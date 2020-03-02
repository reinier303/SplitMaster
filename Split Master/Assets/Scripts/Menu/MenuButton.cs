using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class MenuButton : MonoBehaviour
{
    [SerializeField]
    private string difficultyName;
    [SerializeField]
    private int squareAmount;
    [SerializeField]
    private int splitAmount;
    [SerializeField]
    private int splitCount;
    [SerializeField]
    private bool tutorial;

    public void GoToDifficulty()
    {
        SaveDifficulty();
        GetComponentInParent<Menu>().LoadScene();
    }

    private void SaveDifficulty()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/LevelData.init";

        FileStream stream = new FileStream(path, FileMode.Create);

        LevelData data = new LevelData(difficultyName, squareAmount, splitAmount, splitCount, tutorial);

        formatter.Serialize(stream, data);
        stream.Close();
    }
}

[System.Serializable]
class LevelData
{
    private string difficultyName;
    private int squareAmount;
    private int splitAmount;
    private int splitCount;
    private bool tutorial;

    public LevelData(string newDifficultyName, int newSquareAmount, int newSplitAmount, int newSplitCount, bool isTutorial)
    {
        difficultyName = newDifficultyName;
        squareAmount = newSquareAmount;
        splitAmount = newSplitAmount;
        splitCount = newSplitCount;
        tutorial = isTutorial;
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
