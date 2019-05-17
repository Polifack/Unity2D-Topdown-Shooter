using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Logica del Manageo de inventario.
public class InventoryManager
{
    int _gold;
    List<InventoryItem> _items;

    public InventoryManager(int gold, List<InventoryItem> items)
    {
        //Inicializacion de los valores
        _gold = gold;
        _items = items;

        //Inicializacion de la UI
        if (InventoryUI.instance != null)
            InventoryUI.instance.Setup(this);
    }

    public List<InventoryItem> getInventoryItems()
    {
        return _items;
    }
    public int getGold()
    {
        return _gold;
    }

    public void updateInventory()
    {
        if (InventoryUI.instance != null)
        {
            InventoryUI.instance.Setup(this);
        }
    }
    public void addItem(InventoryItem newItem)
    {
        if (_items.Contains(newItem))
        {
            InventoryItem existingItem = _items[_items.IndexOf(newItem)];
            existingItem.quantity += newItem.quantity;
        }
        else
        {
            _items.Add(newItem);
        }
    }
    public void removeItem(InventoryItem newItem)
    {
        InventoryItem existingItem = _items[_items.IndexOf(newItem)];
        existingItem.quantity -= newItem.quantity;
        if (existingItem.quantity == 0) _items.Remove(existingItem);
    }
}
