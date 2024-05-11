using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class GameState : MonoBehaviour
{
    public static GameState Instance;
    
    public TextMeshProUGUI totalScore;
    public TextMeshProUGUI currentScore;
    public TextMeshProUGUI endMessage;
    
    private int _totalScore;
    public int _currentScore;

    public string _endMessage;
    
    public void Start()
    {
        _totalScore = 0;
        _currentScore = 0;
        _endMessage = "";
    }

    public void Update()
    {
        switch (SceneManager.GetActiveScene().name)
        {
            case "Menu":
                _totalScore += _currentScore;
                _currentScore = 0;
                totalScore.text = "" + _totalScore;
                endMessage.text = _endMessage;
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