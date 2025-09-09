using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.IO;
#if UNITY_EDITOR
using UnityEditor;
#endif
using JetBrains.Annotations;

public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public Text ScoreText;
    public Text PlayerNameMain;
    public Text m_BestScore;
    public static int CountBestScore;
    public GameObject GameOverText;
    
    private bool m_Started = false;
    private static int m_Points;
    
    private bool m_GameOver = false;

    private void Start()
    {
        LoadScore();
        PlayerNameMain.text = MenuManager.Instance.LoadPlayerName();
        UpdateUI();
        m_Points = 0;
        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);
        
        int[] pointCountArray = new [] {1,1,2,2,5,5};
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }
    }

    private void Update()
    {
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
        else if (m_GameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
        UpdateUI();
    }

    public static void AddPoint(int point)
    {
        m_Points += point;
        if (CountBestScore < m_Points)
        {
            CountBestScore = m_Points;
        }
    }
    private void UpdateUI()
    {   
        ScoreText.text = $"Score : {m_Points}";
        m_BestScore.text = $"Best Score : {CountBestScore}";
    }
    public void GameOver()
    {
        m_GameOver = true;
        GameOverText.SetActive(true);
        SaveScore();
    }
    public void Exit()
    {
        SaveScore();
        MenuManager.Instance.SaveName(PlayerNameMain.text);
        #if UNITY_EDITOR
        EditorApplication.isPlaying = false;
        EditorApplication.ExitPlaymode();
        #else
        Application.Quit();
        #endif

    }

    [System.Serializable]
    public class SaveData
    {
        public int CountBestScore;
        public Text PlayerNameMain;
    }
  
    public void SaveScore()
    {
        string filePath = Application.persistentDataPath + "/savefilescore.json";
        SaveData datascore = new SaveData();
        datascore.CountBestScore = CountBestScore;
        string json = JsonUtility.ToJson(datascore);
        File.WriteAllText(filePath, json);
    }
    public void LoadScore()
    {
        string path = Application.persistentDataPath + "/savefilescore.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData datascore = JsonUtility.FromJson<SaveData>(json);
            CountBestScore = datascore.CountBestScore;
        }
    }
}
