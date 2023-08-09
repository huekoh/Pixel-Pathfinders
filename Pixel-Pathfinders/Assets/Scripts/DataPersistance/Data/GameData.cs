using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class GameData
{

    public bool hasGivenItem;
    private const string hasGivenItemKey = "HasGivenItem";
    public Dictionary<string, bool> obelisksDestroyed;
    public InventoryStateData inventoryData;

    //the values in this constructor will be the default values
    //the game start with when there is no data to load
    public GameData()
    {
        hasGivenItem = false;
        obelisksDestroyed = new Dictionary<string, bool>();
        inventoryData = new InventoryStateData();
    }
}
