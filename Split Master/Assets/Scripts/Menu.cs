using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
}
