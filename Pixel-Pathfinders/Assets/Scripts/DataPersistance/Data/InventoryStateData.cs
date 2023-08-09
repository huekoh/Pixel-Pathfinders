using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryStateData
{
    public Dictionary<string, object>[] slots = new Dictionary<string, object>[10];

    public InventoryStateData()
    {
        for (int i = 0; i < slots.Length; i++) 
        {
            slots[i] = new Dictionary<string, object>();
            slots[i].Add("item", null);
            slots[i].Add("amount", 0);
        }

    }
}
