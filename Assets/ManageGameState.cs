using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManageGameState : MonoBehaviour
{
    public TextMeshProUGUI totalScoreText;

    public void Start()
    {
        var gameStates = GameObject.FindGameObjectsWithTag("GameState");

        if (gameStates.Length == 1)
        {
            return;
        }

        foreach (var gameState in gameStates)
        {
            if (gameState.GetComponent<GameState>().isOriginal)
            {
                gameState.GetComponent<GameState>().totalScoreText = totalScoreText;
            }
            else
            {
                Destroy(gameState);
            }
        }
    }

    public void Play()
    {
        SceneManager.LoadScene(1);
    }

    public void Shop()
    {
        SceneManager.LoadScene(2);
    }
}
