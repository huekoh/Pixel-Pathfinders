using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour
{
    private Inventory inventory;
    public int slotNumber;

    private void Start() {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
    }

    private void Update() {
        if (transform.childCount <= 0) {
            inventory.isFull[slotNumber] = false;
        }
    }

    public void DropItem() {
        if (inventory.itemCount[slotNumber] == 1) {
            foreach (Transform child in transform) {
                    child.GetComponent<Spawn>().SpawnDroppedItem();
                    inventory.itemCount[slotNumber]--;
                    inventory.slotHasItems[slotNumber] = false;
                    inventory.itemType[slotNumber] = "";
                    GameObject.Destroy(child.gameObject);
            }

        } else if (inventory.itemCount[slotNumber] > 1) {
            foreach (Transform child in transform) {
                child.GetComponent<Spawn>().SpawnDroppedItem();
                inventory.itemCount[slotNumber]--;
            }
        }
    }
}
