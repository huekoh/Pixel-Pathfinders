using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotion : MonoBehaviour
{
    private Inventory inventory;
    private Slot parentSlot;
    private int slotNumber;

    private void Start() {
        inventory = GameObject.Find("InventoryManager").GetComponent<Inventory>();
        parentSlot = GetComponentInParent<Slot>();
    }

    public void Use() {
        slotNumber = parentSlot.slotNumber;
        //retrieve slotNumber that's already inputted
        if (inventory.itemCount[slotNumber] > 1) {
            Debug.Log("1 Health potion used");
            inventory.itemCount[slotNumber]--;
        } else if (inventory.itemCount[slotNumber] == 1) {
            inventory.itemCount[slotNumber]--;
            inventory.slotHasItems[slotNumber] = false;
            //set that slot to have no items
            inventory.itemType[slotNumber] = "";
            //set that slot itemType back to nothing
            Destroy(gameObject);
        }
    }
}
