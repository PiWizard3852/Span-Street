using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManageGameState : MonoBehaviour
{
    public TextMeshProUGUI totalScoreText;
    
    public void Start()
    {
        var gameStates = GameObject.FindGameObjectsWithTag("GameState");

        if (gameStates.Length == 1) return;

        // Update the original game state and destroy all others
        foreach (var gameState in gameStates)
            if (gameState.GetComponent<GameState>().isOriginal)
                gameState.GetComponent<GameState>().totalScoreText = totalScoreText;
            else
                Destroy(gameState);
    }

    public void Play()
    {
        // Load play mode
        SceneManager.LoadScene(1);
    }

    public void Shop()
    {
        // Load the shop
        SceneManager.LoadScene(2);
    }
}