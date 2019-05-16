using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum GameItemCategories { HEALING, WEAPON}

[System.Serializable]
public class GameItem
{
    public string itemName;
    public Sprite itemIcon;
    public int itemPrice;
    public string itemDescription;
    public bool usable;
    public GameItemCategories category;

    public void onUse()
    {
        Debug.Log("Item " + itemName + " has been used");
    }
    
}
