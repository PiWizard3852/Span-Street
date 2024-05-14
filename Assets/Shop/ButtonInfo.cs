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

    void Update()
    {
        PriceText.text = "Price: $" + ShopManager.GetComponent<ShopManager>().shopItems[2, ItemID].ToString();
    }
}
