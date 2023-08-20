using System.Collections;
using System.Collections.Generic;
using System.IO;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class Menu : MonoBehaviour
{
    public static Menu Instance;
    public TMP_InputField iField;
    public TextMeshProUGUI bestScoreText;
    public string playerName;

    public string bestPlayerName;
    public int bestScore = 0;

    [System.Serializable]
    class SaveData
    {
        public string bestPlayerName;
        public int bestScore;
    }

    public void SaveScore(int m_Points)
    {
        SaveData data = new SaveData();
        data.bestPlayerName = playerName;
        data.bestScore = m_Points;

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/savefile2.json", json);
    }

    public void LoadScore()
    {
        string path = Application.persistentDataPath + "/savefile2.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            bestPlayerName = data.bestPlayerName;
            bestScore = data.bestScore;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        LoadScore();
        bestScoreText.text = $"Best Score : {bestPlayerName} : {bestScore}";

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartNew()
    {
        gameObject.SetActive(false);
        SceneManager.LoadScene(1);
    }

    public void Exit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
        //MainManager.Instance.SaveColor();
    }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);        
    }

    public void InputField()
    {
        playerName = iField.text;
    }
}
