using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerHealthManager : MonoBehaviour
{
    public PlayerHealthData playerHealthData;
    private PlayerHealthBarUI playerHealthBar;
    public GameObject healthTextPrefab;
    private float health;
    private float maxHealth;
    // Start is called before the first frame update
    void Start()
    {
        health = playerHealthData.health;
        maxHealth = playerHealthData.maxHealth;
        playerHealthBar = GetComponentInChildren<PlayerHealthBarUI>();
    }

    // Update is called once per frame
    void Update()
    {
        health = playerHealthData.health;
        playerHealthBar.SetHealth(health);
    }

    public void ConsumeFoodItem(InventorySlot foodItem)
    {
        int healthRestoration = foodItem.itemObject.data.itemValue;
        if (health < maxHealth)
        {
            foodItem.ReduceAmount(1);
            health += healthRestoration;
            playerHealthData.health = health;
            if (healthTextPrefab)
            {
            ShowHealthText(healthRestoration);
            }
        }
        
    }

    void ShowHealthText(float damage)
    {
        var text = Instantiate(healthTextPrefab, transform.position, Quaternion.identity, transform);
        text.GetComponent<TextMeshPro>().text = damage.ToString();
        text.GetComponent<TextMeshPro>().color = Color.green;
    }
}
