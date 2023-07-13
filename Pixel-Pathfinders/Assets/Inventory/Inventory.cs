using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public GameObject[] slots; //{ get; private set; }
    public Button[] removeButtons; //{ get; private set; }
    public bool[] isFull;
    public bool[] slotHasItems;
    public int[] itemCount;
    public string[] itemType;
    public int maxItemCount = 5;
    private static Inventory instance;

    void Start() {
        removeButtons = new Button[10];
        slots = new GameObject[10];

        GameObject itemsParentObject = GameObject.Find("Canvas/Inventory/ItemsParent");
        if (itemsParentObject != null)
        {
            Transform itemsParentTransform = itemsParentObject.transform;
            // Find the inventory slots dynamically
            for (int i = 0; i < 10; i++)
            {
                string slotName = "Inventory Slot";
                if (i > 0)
                {
                    slotName += " (" + i + ")";
                }

                Transform slotTransform = itemsParentTransform.Find(slotName);
                if (slotTransform != null)
                {
                    Transform removeButtonTransform = slotTransform.Find("Remove Button");
                    GameObject removeButtonObject = removeButtonTransform.gameObject;
                    removeButtons[i] = removeButtonObject.GetComponent<Button>();

                    Transform iconTransform = slotTransform.Find("Slot image/Icon");
                    GameObject iconObject = iconTransform.gameObject;
                    slots[i] = iconObject;
                }
                else
                {
                    Debug.LogError(slotName + " not found.");
                }
            }
        } else {
            Debug.LogError("itemsParentObject is null");
        }
    }

    void Update() {
        for (int i = 0; i < isFull.Length; i++) {
            if (itemCount[i] > 0) {
                removeButtons[i].interactable = true;
            } else if (itemCount[i] <= 0) {
                removeButtons[i].interactable = false;
            }
        }
    }
}
