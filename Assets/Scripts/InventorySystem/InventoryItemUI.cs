using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventoryItemUI : MonoBehaviour
{
    public Button ItemButton;
    public Text ItemName;
    public Text ItemValue;
    public Image ItemImage;
    public Text ItemQuantity;

    private GameItem item;

    public void Setup(InventoryItem sourceItem)
    {
        item = sourceItem.item;

        ItemName.text = item.itemName;
        ItemValue.text = item.itemPrice.ToString();
        ItemImage.sprite = item.itemIcon;
        ItemQuantity.text = sourceItem.quantity.ToString();        
    }

    public void OnClick()
    {
        Debug.Log("Clicked!");
    }
}
