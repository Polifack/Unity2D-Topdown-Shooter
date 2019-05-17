using System.Collections.Generic;
using UnityEngine;

//Visuals del Manageo de Inventario
public class InventoryUI : MonoBehaviour
{
    public static InventoryUI instance;

    public GameObject InventoryItemButton;

    InventoryManager _inventoryManager;
    List<GameObject> _inventoryItemButtons = new List<GameObject>();

    private void Awake()
    {
        instance = this;
    }
    public void Setup(InventoryManager inventoryManager)
    {
        this._inventoryManager = inventoryManager;

        foreach (InventoryItem item in _inventoryManager.getInventoryItems())
        {
            GameObject temp = Instantiate(InventoryItemButton, transform);
            temp.GetComponent<InventoryItemUI>().Setup(item);
            _inventoryItemButtons.Add(temp);
        }
    }

    private void OnDisable()
    {
        foreach (GameObject button in _inventoryItemButtons)
        {
            Destroy(button);
        }
    }

}
