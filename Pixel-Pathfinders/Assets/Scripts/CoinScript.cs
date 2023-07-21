using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinScript : MonoBehaviour
{
    private TextMeshProUGUI coinAmount;
    public InventoryObject coinInventory;
    // Start is called before the first frame update
    void Start()
    {
        coinAmount = gameObject.GetComponentInChildren<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateCoinText();
    }

    private void UpdateCoinText()
    {
        coinAmount.text = coinInventory.Container.Items[0].amount.ToString();
    }
}