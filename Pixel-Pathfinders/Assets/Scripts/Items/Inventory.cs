using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public GameObject[] slots;
    public Button[] removeButton;
    public bool[] isFull;
    public bool[] slotHasItems;
    public int[] itemCount;
    public string[] itemType;
    public int maxItemCount = 5;

    void Update() {
        for (int i = 0; i < isFull.Length; i++) {
            if (itemCount[i] > 0) {
                removeButton[i].interactable = true;
            } else if (itemCount[i] <= 0) {
                removeButton[i].interactable = false;
            }
        }
    }
}
