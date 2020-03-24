using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class Menu : MonoBehaviour
{
    [Header("Important: Check build settings before changing value!")]
    [SerializeField]
    private int gameScene;
    [SerializeField]
    private GameObject LoadingScreen, MenuObject;

    public void LoadScene(int scene = 0)
    {
        if(SceneManager.GetActiveScene().buildIndex == 0)
        {
            MenuObject.SetActive(false);
        }
        LoadingScreen.SetActive(true);
        SceneManager.LoadSceneAsync(scene);
    }

    public void WipeData()
    {
        string path = Application.persistentDataPath + "/saveData/AchievementData.sav";
        if (File.Exists(path))
        {
            File.Delete(path);
            Debug.Log("File at: " + path + " deleted.");
            LoadScene(0);
        }
        else
        {
            Debug.Log("No file found at: " + path);
        }
    }
}
