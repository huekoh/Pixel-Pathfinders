using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGiver : MonoBehaviour, IDataPersistence
{
    [SerializeField] private TextAsset inkJSON;
    [SerializeField] private InventoryObject inventory;
    [SerializeField] public ItemObject itemToGive;
    [SerializeField] private GameObject visualCue;
    [SerializeField] private GameObject visual;
    [SerializeField] private GameObject visual2;
    public bool hasGivenItem;
    private bool playerInRange;
    

    private void Awake() {
        playerInRange = false;
        visualCue.SetActive(false);
    }

    private void Start() {}

    public void LoadData(GameData data)
    {
        this.hasGivenItem = data.hasGivenItem;
    }

    public void SaveData(ref GameData data)
    {
        data.hasGivenItem = this.hasGivenItem;
    }

    private void Update() {
        if (!hasGivenItem)
        {
            visual.SetActive(true);
            visual2.SetActive(false);
            if (playerInRange) {
                visualCue.SetActive(true);
                if (Input.GetKeyDown(KeyCode.E)) {
                    hasGivenItem = true;
                    inventory.AddItem(itemToGive.CreateItem(), 1);
                    visualCue.SetActive(false);
                    DialogueManager.GetInstance().EnterDialogueMode(inkJSON);
                }
            } else {
                visualCue.SetActive(false);
            }
        }
        else
        {
            visual2.SetActive(true);
            visual.SetActive(false);
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
    }
}