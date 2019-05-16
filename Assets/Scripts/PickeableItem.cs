using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickeableItem : MonoBehaviour
{
    public GameItem gameItem;
    public int quantity;
    public void onPlayerEnter(Player player)
    {
        player.AddItem(new InventoryItem(gameItem, quantity));
        Destroy(gameObject);
    }
}
