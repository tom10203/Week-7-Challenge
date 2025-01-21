using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

public class DataPersistance : MonoBehaviour
{
    public TMP_InputField nameInput;
    public int highScore;
    public static DataPersistance instance { get; private set; }
    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }

        else
        {
            instance = this;

            DontDestroyOnLoad(gameObject);
        }
    }

    // Update is called once per frame
    public void StartGame()
    {
        SceneManager.LoadScene(1);
        
    }

    [System.Serializable]
    class DataToSave
    {
        public string _name;
        public int _highScore;
    }

    public void SaveData()
    {

        DataToSave dataToSave = new DataToSave();
        dataToSave._name = nameInput.text;
        dataToSave._highScore = highScore;
        string json = JsonUtility.ToJson(dataToSave);
        File.WriteAllText(Application.persistentDataPath + "/playerData.json", json);
    }

    public void LoadData()
    {

        string filePath = Application.persistentDataPath + "/playerData.json";
        if (File.Exists(filePath))
        {
            string data = File.ReadAllText(filePath);
            DataToSave loadedData = JsonUtility.FromJson<DataToSave>(data);
            nameInput.text = loadedData._name;
            highScore = loadedData._highScore;
        }
        else
        {
            nameInput.text = "Tom";
            highScore = 0;
        }

    }
}
