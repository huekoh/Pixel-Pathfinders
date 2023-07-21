using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopManager : MonoBehaviour
{   
    private int coins;
    public ItemObject[] itemObjectSO;
    public GameObject[] shopPanelsGO;
    public ShopTemplate[] shopPanels;
    public Button[] purchaseButtons;
    public InventoryObject coinInventory;
    public InventoryObject inventory;
    private Item newItem;

    // Start is called before the first frame update
    
    void Start()
    {
        for (int i = 0; i < itemObjectSO.Length; i++)
        {
            shopPanelsGO[i].SetActive(true);
        }
        coins = coinInventory.Container.Items[0].amount;
        LoadPanels();
        CheckPurchaseable();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void AddCoins()
    {
        coins++;
        coinInventory.Container.Items[0].amount = coins;
        CheckPurchaseable();
    }

    public void CheckPurchaseable()
    {
        for (int i = 0; i < itemObjectSO.Length; i++)
        {
            if (coins >= itemObjectSO[i].baseCost)
            {
                purchaseButtons[i].interactable = true;
            } else {
                purchaseButtons[i].interactable = false;
            }
        }
    }

    public void PurchaseItem(int buttonNum)
    {
        if (itemObjectSO[buttonNum].stackable || inventory.EmptySlotCount > 0)
        {
            coins = coins - itemObjectSO[buttonNum].baseCost;
            coinInventory.Container.Items[0].amount = coins;
            newItem = itemObjectSO[buttonNum].CreateItem();
            inventory.AddItem(newItem, 1);
            CheckPurchaseable();
        }
    }

    public void LoadPanels()
    {
        for (int i = 0; i < itemObjectSO.Length; i++)
        {
            shopPanels[i].titleTxt.text = itemObjectSO[i].title;
            shopPanels[i].descriptionTxt.text = itemObjectSO[i].description;
            shopPanels[i].costTxt.text = "Coins: " + itemObjectSO[i].baseCost.ToString();

        }
    }
}
