using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


public class SaveEditor : EditorWindow
{
    public static Dictionary<string, float> Stats = new Dictionary<string, float>();
    public static Dictionary<string, bool> Unlockables = new Dictionary<string, bool>();


    [MenuItem("Tools/Save Editor")]
    private static void Init()
    {
        var window = (SaveEditor)GetWindow(typeof(SaveEditor));
        window.minSize = new Vector2(400, 300);
    }


    private void OnGUI()
    {
        if(GUILayout.Button("Delete Achievement Data"))
        {
            EditorGUILayout.LabelField("Save Editor", EditorStyles.boldLabel);
            EditorGUILayout.LabelField("\n");
            string path = Application.persistentDataPath + "/saveData/AchievementData.sav";
            if (File.Exists(path))
            {
                File.Delete(path);
                Debug.Log("File at: " + path + " deleted.");
            }
            else 
            {
                Debug.Log("No file found at: " + path);                
            }
        }
    }
}
