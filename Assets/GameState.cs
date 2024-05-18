using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameState : MonoBehaviour
{
    public static GameState Instance;
    public TextMeshProUGUI totalScoreText;
    public TextMeshProUGUI currentScoreText;
    public int totalScore;
    public int currentScore;
    public bool isOriginal = false;

    
    public void Start()
    {
        totalScore = PlayerPrefs.GetInt("totalScore", 0);
        currentScore = 0;
        
        totalScoreText.text = "" + totalScore;
    }

    public void Update()
    {
        switch (SceneManager.GetActiveScene().name)
        {
            case "Menu":
                totalScoreText.text = "" + totalScore;
                break;
            case "Game":
                currentScoreText.text = "" + currentScore;
                break;
        }
    }

    public void OnApplicationQuit()
    {
        PlayerPrefs.SetInt("totalScore", totalScore);
        PlayerPrefs.Save();
    }

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
}