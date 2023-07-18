using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public MouseItem mouseItem = new MouseItem();
    public InventoryObject inventory;

    public void OnTriggerEnter2D(Collider2D other) {
        var item = other.GetComponent<GroundItem>();
        if (item) {
            inventory.AddItem(new Item(item.item), 1);
            Destroy(other.gameObject);
        }
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            inventory.Save();
        }

        if (Input.GetKeyDown(KeyCode.I)) {
            inventory.Load();
        }
    }

    //set container size to 0
    private void OnApplicationQuit() {
        inventory.Container.Items = new InventorySlot[10];
    }
}
