using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class GameState : MonoBehaviour
{
    public static GameState Instance;
    
    public TextMeshProUGUI totalScore;
    public TextMeshProUGUI currentScore;
    
    private int _totalScore;
    public int _currentScore;
    
    public void Start()
    {
        _totalScore = 0;
        _currentScore = 0;
        
        // SceneManager.LoadScene(0);
    }

    public void Update()
    {
        switch (SceneManager.GetActiveScene().name)
        {
            case "Menu":
                _totalScore += _currentScore;
                _currentScore = 0;
        
                totalScore.text = "" + _totalScore;
                
                break;
            case "Game":
                currentScore.text = "" + _currentScore;
                break;
        }
    }

    public void Play()
    {
        SceneManager.LoadScene(1);
    }
        
    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
}