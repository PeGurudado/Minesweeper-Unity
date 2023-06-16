using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ConfigurationLoader : MonoBehaviour
{
    public static BoardConfig LoadedBoardConfig;
    private const string JSON_FILE_NAME = "board.json";
    private string jsonFilePath;


    private void Awake()
    {
        jsonFilePath = Path.Combine(Application.streamingAssetsPath, JSON_FILE_NAME);

        LoadConfigFromJson();
    }

    void LoadConfigFromJson(){
        if (File.Exists(jsonFilePath))
        {
            string json = File.ReadAllText(jsonFilePath);
            LoadedBoardConfig = JsonUtility.FromJson<BoardConfig>(json);
        }
    }
}
