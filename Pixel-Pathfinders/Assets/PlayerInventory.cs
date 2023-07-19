﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public InventoryObject inventory;
    public InventoryObject equipment;

    public void OnTriggerEnter2D(Collider2D other)
    {
        var groundItem = other.GetComponent<GroundItem>();
        if (groundItem)
        {
            Item _item = new Item(groundItem.item);
            if (inventory.AddItem(_item, 1)) 
            {
                Destroy(other.gameObject);
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