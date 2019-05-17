using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class PickeableItem : MonoBehaviour
{
    public GameItem gameItem;
    public int quantity;

    SpriteRenderer sr;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.sprite = gameItem.itemIcon;
    }

    public void onPlayerEnter(Player player)
    {
        player.AddItem(new InventoryItem(gameItem, quantity));
        Destroy(gameObject);
    }
}
