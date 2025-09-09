using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System.Collections;
using static UnityEngine.Analytics.IAnalytic;
using UnityEngine.EventSystems;
using UnityEngine.UI;
[System.Serializable]
public class PlayerData
    {
        public string playerName;
    }
public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;
    public Button startButton;
    public TMP_InputField nameInput;  
    public void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        startButton.gameObject.SetActive(true);
        startButton.interactable = true;
        string savedName = LoadPlayerName();
        if(!string.IsNullOrEmpty(savedName))
        {
            nameInput.text = savedName;
        }
    }

    public void SaveName(string playerName)
    {
        if (!string.IsNullOrEmpty(playerName))
        {
            File.WriteAllText(Application.persistentDataPath + "/playername.json", playerName);
        }
    }
    public string LoadPlayerName()
    {
        string filePath = Application.persistentDataPath + "/playername.json";
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            if (json!=null)
            {
                if (json.Length > 0)
                {
                    return json;
                }
            }         
        }

        return ""; 
    }
    
    public void StartNew()
    {
        SaveName(nameInput.text);
        SceneManager.LoadScene(1);
    }

}
