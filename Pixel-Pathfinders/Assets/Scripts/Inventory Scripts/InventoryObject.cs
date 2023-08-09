using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEditor;
using System.Runtime.Serialization;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory System/Inventory")]
public class InventoryObject : ScriptableObject //, IDataPersistence
{
    public string savePath;
    public ItemDatabaseObject database;
    public Inventory Container;

    // public void LoadData(GameData data)
    // {
    //     for (int i = 0; i < Container.Slots.Length; i++)
    //     {
    //         Container.Slots[i] = data.inventoryData.slots[i].TryGetValue("item", out value);
    //     }
    // }

    // public void SaveData(ref GameData data)
    // {

    // }

    public bool AddItem(Item _item, int _amount) 
    {
        InventorySlot slot = FindItemOnInventory(_item);
    
        if (EmptySlotCount <= 0) {
            if (database.ItemObjects[_item.Id].stackable)
                {
                slot.AddAmount(_amount);
                return true;
                }
            return false;
        }

        if (!database.ItemObjects[_item.Id].stackable || slot == null)
        {
            SetEmptySlot(_item, _amount);
            return true;
        }
        slot.AddAmount(_amount);
        return true;
    }

    public int EmptySlotCount
    {
        get
        {
            int counter = 0;
            for (int i = 0; i < Container.Slots.Length; i++)
            {
                if (Container.Slots[i].item.Id <= -1)
                    counter++;
            }
            return counter;
        }
    }

    public InventorySlot FindItemOnInventory(Item _item)
    {
        for (int i = 0; i < Container.Slots.Length; i++)
        {
            if (Container.Slots[i].item.Id == _item.Id)
            {
                return Container.Slots[i];
            }
        }
        return null;
    }

    public InventorySlot SetEmptySlot(Item _item, int _amount)
    {
        for (int i = 0; i < Container.Slots.Length; i++) {
            if (Container.Slots[i].item.Id <= -1) {
                Container.Slots[i].UpdateSlot(_item, _amount);
                return Container.Slots[i];
            }
        }
        return null;
    }

    public void SwapItems(InventorySlot item1, InventorySlot item2)
    {
        if (item2.CanPlaceInSlot(item1.itemObject) && item1.CanPlaceInSlot(item2.itemObject))
        {
        InventorySlot temp = new InventorySlot(item2.item, item2.amount);
        item2.UpdateSlot(item1.item, item1.amount);
        item1.UpdateSlot(temp.item, temp.amount);
        }
    }

    public void RemoveItem(Item _item)
    {
        for (int i = 0; i < Container.Slots.Length; i++) {
            if (Container.Slots[i].item == _item) {
                Container.Slots[i].UpdateSlot(null, 0);
            }
        }
    }

    /*[ContextMenu("Save")]
    public void Save()
    {
        IFormatter formatter = new BinaryFormatter();
        Stream stream = new FileStream(string.Concat(Application.persistentDataPath, savePath), FileMode.Create, FileAccess.Write);
        formatter.Serialize(stream, Container);
        stream.Close();
    }

    [ContextMenu("Load")]
    public void Load()
    {
        if (File.Exists(string.Concat(Application.persistentDataPath, savePath))) {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(string.Concat(Application.persistentDataPath, savePath), FileMode.Open, FileAccess.Read);
            Inventory newContainer = (Inventory)formatter.Deserialize(stream);
            for (int i = 0; i < Container.Items.Length; i++) {
                Container.Items[i].UpdateSlot(newContainer.Items[i].item, newContainer.Items[i].amount);
            }
            stream.Close();
        }
    }*/

    [ContextMenu("Clear")]
    public void Clear()
    {
        Container.Clear();
    }
}

[System.Serializable]
public class Inventory
{
    public InventorySlot[] Slots = new InventorySlot[10];
    public void Clear() {
        for (int i = 0; i < Slots.Length; i++) 
        {
            Slots[i].RemoveItem();
        }
    }
}

[System.Serializable]
public class InventorySlot
{
    public ItemType[] AllowedItems = new ItemType[0];
    [System.NonSerialized]
    public UserInterface parent;
    public Item item;
    public int amount;

    public ItemObject itemObject
    {
        get
        {
            if (item.Id >= 0)
            {
                return parent.inventory.database.ItemObjects[item.Id];
            }
            return null;
        }
    }
    public InventorySlot() 
    {
        UpdateSlot(new Item(), 0);
    }
    public InventorySlot(Item _item, int _amount) 
    {
        UpdateSlot(_item, _amount);
    }
    public void UpdateSlot(Item _item, int _amount) 
    {
        item = _item;
        amount = _amount;
    }
    public void RemoveItem()
    {
        UpdateSlot(new Item(), 0);
    }
    public void AddAmount(int value) 
    {
        UpdateSlot(item, amount += value);
    }
    public void ReduceAmount(int value)
    {
       UpdateSlot(item, amount -= value);
    }
    public bool CanPlaceInSlot(ItemObject _itemObject) 
    {
        if (AllowedItems.Length <= 0 || _itemObject == null || _itemObject.data.Id < 0) 
            return true;
        for (int i = 0; i < AllowedItems.Length; i++) 
        {
            if (_itemObject.type == AllowedItems[i])
            return true;
        }
        return false;
    }
}