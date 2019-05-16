using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public static InventoryUI instance;
    public GameObject InventoryItemButton;
    public List<GameObject> DestroyOnDisable;

    private void Awake()
    {
        instance = this;
    }

    public void ShowInventory(List<InventoryItem> inventory)
    {
        foreach (InventoryItem item in inventory)
        {
            GameObject temp = Instantiate(InventoryItemButton, transform);
            temp.GetComponent<InventoryItemUI>().Setup(item);
            DestroyOnDisable.Add(temp);
        }
    }

    private void OnDisable()
    {
        foreach (GameObject button in DestroyOnDisable)
        {
            Destroy(button);
        }
    }

}
