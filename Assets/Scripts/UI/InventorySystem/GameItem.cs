using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "TopdownShooter/Item", order = 1)]
public class GameItem : ScriptableObject
{
    public string itemName;
    public Sprite itemIcon;
    public int itemPrice;
    public string itemDescription;
    public bool usable;

    public void onUse()
    {
        Debug.Log("Item " + itemName + " has been used");
    }
    
}
