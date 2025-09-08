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
    void Start()
    {
        string savedName = LoadPlayerName();
        if(!string.IsNullOrEmpty(savedName))
        {
            nameInput.text = savedName;
        }
        StartCoroutine(ActivateInputNextFrame());
    }
    private IEnumerator ActivateInputNextFrame()
    {
        yield return null; // wait 1 frame
        nameInput.ActivateInputField(); // focus the field
        nameInput.Select();              // make TMP think it's selected
        nameInput.caretPosition = nameInput.text.Length;
    }
    public void SaveName()
    {
        string playerName = nameInput.text;
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
        SaveName();
        SceneManager.LoadScene(1);
    }

}
