using System.Collections;
using System.Collections.Generic;
using Shop;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemInfo : MonoBehaviour
{
    public int ItemID;
    public TextMeshProUGUI PriceText;
    public GameObject ShopManager;
    public bool isBought = false;
    public bool isEquipped = false;
}
