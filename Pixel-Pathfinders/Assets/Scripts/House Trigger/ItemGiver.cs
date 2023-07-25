using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGiver : MonoBehaviour
{
    [SerializeField] private InventoryObject inventory;
    [SerializeField] public ItemObject itemToGive;
    [SerializeField] private GameObject visualCue;
    public bool hasGivenItem { get; private set; }
    private bool playerInRange;
    private const string hasGivenItemKey = "HasGivenItem";

    private void Awake() {
        playerInRange = false;
        hasGivenItem = false;
        visualCue.SetActive(false);
    }
    void Start()
    {
        hasGivenItem = PlayerPrefs.GetInt(hasGivenItemKey, 0) == 1;

        if (hasGivenItem)
        {
            Debug.Log("The item has already been given.");
        }
    }

    private void Update() {
        if (!hasGivenItem)
            if (playerInRange) {
                visualCue.SetActive(true);
                if (Input.GetKeyDown(KeyCode.E)) {
                    hasGivenItem = true;
                    PlayerPrefs.SetInt(hasGivenItemKey, 1);
                    PlayerPrefs.Save();
                    inventory.AddItem(itemToGive.CreateItem(), 1);
                    Debug.Log("item given.");
                    visualCue.SetActive(false);
                }
            } else {
                visualCue.SetActive(false);
            }
    }

    private void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.tag == "Player") {
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D col) {
        if (col.gameObject.tag == "Player") {
            playerInRange = false;
        }
    }

    public void Reset()
    {
        hasGivenItem = false;

        PlayerPrefs.SetInt(hasGivenItemKey, 0);
        PlayerPrefs.Save();
    }
}