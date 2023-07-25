using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DynamicInterface : UserInterface
{
    public GameObject inventoryPrefab;
    private const float doubleClickTimeThreshold = 0.3f; // Set the threshold for double-click time
    private float lastClickTime = 0f;
    private GameObject lastClickedObject = null;
    public override void CreateSlots() {
        slotsOnInterface = new Dictionary<GameObject, InventorySlot>();
        for (int i = 0; i < inventory.Container.Slots.Length; i++) {
            var obj = Instantiate(inventoryPrefab, Vector3.zero, Quaternion.identity, transform);

        
            AddEvent(obj, EventTriggerType.PointerEnter, delegate { OnEnter(obj); });
            AddEvent(obj, EventTriggerType.PointerExit, delegate { OnExit(obj); });
            AddEvent(obj, EventTriggerType.BeginDrag, delegate { OnDragStart(obj); });
            AddEvent(obj, EventTriggerType.EndDrag, delegate { OnDragEnd(obj); });
            AddEvent(obj, EventTriggerType.Drag, delegate { OnDrag(obj); });
            AddEvent(obj, EventTriggerType.PointerClick, delegate { OnPointerClick(obj); });

            slotsOnInterface.Add(obj, inventory.Container.Slots[i]);
        }
    }

    public void OnPointerClick(GameObject obj)
    {
        // Check if the item is a food item before processing double-click
        if (slotsOnInterface.TryGetValue(obj, out var slotData) && slotData.itemObject != null)
        {
            if (slotData.itemObject.type == ItemType.Food)
            {
                // Check if the same object is double-clicked within the double-click time threshold
                if (lastClickedObject == obj && Time.time - lastClickTime <= doubleClickTimeThreshold)
                {
                    // Perform the double-click action here
                    PlayerHealthManager playerHealthManager = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealthManager>();
                    playerHealthManager.ConsumeFoodItem(slotData);
                    if (slotData.amount == 0)
                    {
                        slotData.RemoveItem();
                    }
                    lastClickedObject = null;
                }
                else
                {
                    lastClickedObject = obj;
                    lastClickTime = Time.time;
                }
            }
        }
    }
}