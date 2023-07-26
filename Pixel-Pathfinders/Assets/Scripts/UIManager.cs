using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject inventoryScreen;
    [SerializeField] private GameObject equipmentScreen;
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private GameObject playerHealthBar;
    private GameObject shop;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        ShopManager shopManager = FindObjectOfType<ShopManager>();
        if (shopManager != null)
        {
            shop = shopManager.gameObject;
        }

        if (dialoguePanel.activeInHierarchy)
        {
            inventoryScreen.SetActive(false);
            equipmentScreen.SetActive(false);
        }
        else
        {
            inventoryScreen.SetActive(true);
            equipmentScreen.SetActive(true);
        }

        if (shop != null)
        {
            if (shop.activeInHierarchy)
            {
                playerHealthBar.SetActive(false);
            }
            else
            {
                playerHealthBar.SetActive(true);
            }
        }
    }
}
