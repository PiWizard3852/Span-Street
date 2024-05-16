using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ButtonInfo : MonoBehaviour
{
    public int ItemID;
    public TextMeshProUGUI PriceText;
    public GameObject ShopManager;
    public bool isBought = false;

    public void Start()
    {
        PriceText.text = "Price: $" + ShopManager.GetComponent<ShopManager>().shopItems[0, ItemID].ToString();
    }
}
