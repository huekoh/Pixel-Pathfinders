using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    private Inventory inventory;
    public GameObject itemButton;
    private bool playerInRange;
    public string typeOfItem;
    private bool isPickedUp;

    // Start is called before the first frame update
    void Start()
    {
        playerInRange = false;
        isPickedUp = false;
        inventory = GameObject.Find("InventoryManager").GetComponent<Inventory>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerInRange) {
            if (Input.GetKeyDown(KeyCode.E) && !isPickedUp) {
                for (int i = 0; i < inventory.slots.Length; i++) {
                    if (inventory.isFull[i] == false) {
                        // Item can be added to inventory
                        if (inventory.slotHasItems[i] == false) {
                            inventory.itemCount[i]++;
                            inventory.itemType[i] = typeOfItem;
                            inventory.slotHasItems[i] = true;
                            Instantiate(itemButton, inventory.slots[i].transform, false);
                        } else if (inventory.itemType[i] == typeOfItem) {
                            inventory.itemCount[i]++;
                            //Debug.Log("Item count " + inventory.itemCount[i]);
                        } else {
                            continue;
                        }

                        if (inventory.itemCount[i] >= inventory.maxItemCount) {
                            inventory.isFull[i] = true;
                            Destroy(gameObject);
                        } else {
                            Destroy(gameObject);
                        }
                        isPickedUp = true;
                        break;
                    }
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.tag == "Player" && !isPickedUp) {
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player") {
            playerInRange = false;
        }
    }
}
