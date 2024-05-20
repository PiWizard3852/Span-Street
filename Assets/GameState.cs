using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class GameState : MonoBehaviour
{
    public static GameState Instance;
    
    public TextMeshProUGUI totalScoreText;
    public TextMeshProUGUI currentScoreText;
    
    public int totalScore;
    public int currentScore;
    
    public bool isOriginal = false;
    
    public readonly Material[] Skins = new Material[5];
    public readonly int[] SkinPrices = new int[5];
    
    public Material skin1;
    public Material skin2;
    public Material skin3;
    public Material skin4;
    public Material skin5;
    
    public Material currentSkin;
    
    public void Start()
    {
        totalScore = PlayerPrefs.GetInt("totalScore", 0);
        currentScore = 0;
        
        totalScoreText.text = "" + totalScore;
        
        SkinPrices[0] = 10;
        SkinPrices[1] = 20;
        SkinPrices[2] = 30;
        SkinPrices[3] = 40;
        SkinPrices[4] = 50;

        Skins[0] = skin1;
        Skins[1] = skin2;
        Skins[2] = skin3;
        Skins[3] = skin4;
        Skins[4] = skin5;

        currentSkin = Skins[PlayerPrefs.GetInt("CurrentSkin")] ?? skin1;
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