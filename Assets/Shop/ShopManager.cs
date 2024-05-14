using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopManager : MonoBehaviour
{
    public int[,] shopItems = new int[5,5];
    public float coins;
    public TextMeshProUGUI CoinsText;
    private GameState _gameState;
    private ButtonInfo _buttonInfo;
    public GameObject _player;

    public GameObject skin1;
    public GameObject skin2;
    public GameObject skin3;
    
    public void Start()
    {
         _gameState = GameObject.FindGameObjectWithTag("GameState").gameObject.GetComponent<GameState>();
        coins = _gameState.totalScore;
        CoinsText.text = "Coins: " + coins;

        _buttonInfo = GameObject.FindGameObjectWithTag("Button").gameObject.GetComponent<ButtonInfo>();

        _player = GameObject.FindGameObjectWithTag("Player").gameObject;

        shopItems[1,1] = 1;
        shopItems[1,2] = 2;
        shopItems[1,3] = 3;
        shopItems[1,4] = 4;

        shopItems[2,1] = 10;
        shopItems[3,2] = 20;
        shopItems[4,3] = 30;
        shopItems[5,4] = 40;
    }

    public void Buy()
    {
        var buttonID = _buttonInfo.ItemID;

        if(coins >= shopItems[2, buttonID]);
        {
            coins -= shopItems[2, buttonID];
            CoinsText.text = "Coins: " + coins.ToString();
            _buttonInfo.isBought = true;
        }
    }
}
