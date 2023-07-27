using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootBag : MonoBehaviour
{
    public GroundItem droppedItemPrefab;
    public List<ItemObject> itemDropList = new List<ItemObject>();

    GroundItem GetDroppedItem()
    {
        int randomNumber = Random.Range(1, 31);
        List<ItemObject> possibleItems = new List<ItemObject>();
        foreach (ItemObject item in itemDropList)
        {
            if (randomNumber <= item.dropChance)
            {
                possibleItems.Add(item);
            }

            if (possibleItems.Count > 0)
            {
                ItemObject droppedItem = possibleItems[Random.Range(0, possibleItems.Count)];
                droppedItemPrefab.item = droppedItem;
                return droppedItemPrefab;
            }
        }
        Debug.Log("No loot dropped");
        return null;
    }

    public void InstantiateLoot(Vector3 spawnPosition)
    {
        GroundItem droppedItem = GetDroppedItem();
        if (droppedItem != null)
        {
            GroundItem lootGameObject = Instantiate(droppedItemPrefab, spawnPosition, Quaternion.identity);
            lootGameObject.GetComponent<SpriteRenderer>().sprite = droppedItem.item.uiDisplay;
        }
    }
}
