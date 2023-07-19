using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType {
    Food,
    Weapon,
    Shield,
    Default
}

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory System/Items/item")]
public class ItemObject : ScriptableObject
{
    public Sprite uiDisplay;
    public bool stackable;
    public ItemType type;
    [TextArea(15,20)]
    public string description;
    public Item data = new Item();

    public Item CreateItem() {
        Item newItem = new Item(this);
        return newItem;
    }
}

[System.Serializable]
public class Item {
    public string Name;
    public int Id = -1;
    public ItemBuff[] buffs;
    public Item() {
        Name = "";
        Id = -1;
    }
    public Item(ItemObject item) {
        Name = item.name;
        Id = item.data.Id;
        buffs = new ItemBuff[item.data.buffs.Length];
        for (int i = 0; i < buffs.Length; i++) {
            buffs[i] = new ItemBuff(item.data.buffs[i].value);
        }
    }
}

[System.Serializable]
public class ItemBuff {
    public int value;
    public ItemBuff(int _value) {
        value = _value;
    }
}