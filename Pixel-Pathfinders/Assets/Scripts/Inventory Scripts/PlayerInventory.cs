using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public InventoryObject inventory;
    public InventoryObject coinInventory;

    public void OnTriggerEnter2D(Collider2D other)
    {
        var groundItem = other.GetComponent<GroundItem>();
        if (groundItem)
        {
            Item _item = new Item(groundItem.item);

            if (_item.Name == "Coin")
            {
                coinInventory.Container.Slots[0].AddAmount(1);
                Destroy(other.gameObject);
            } else {
                if (inventory.AddItem(_item, 1)) 
                    {
                        Destroy(other.gameObject);
                    }
            }
        }
    }
    private void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.Space))
        {
            inventory.Save();
            equipment.Save();
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            inventory.Load();
            equipment.Load();
        }*/
    }
    private void OnApplicationQuit()
    {
        /*inventory.Container.Clear();
        equipment.Container.Clear();*/
    }
}