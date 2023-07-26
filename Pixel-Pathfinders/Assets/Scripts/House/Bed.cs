using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bed : MonoBehaviour
{
    [Header("Player Health")]
    [SerializeField] private PlayerHealthData playerHealthData;

    private void OnEnable()
    {
        DialogueManager.OnChoiceMade += HandleChoiceMade;
    }

    private void OnDisable()
    {
        DialogueManager.OnChoiceMade -= HandleChoiceMade;
    }

    private void HandleChoiceMade(int choiceIndex)
    {
        if (choiceIndex == 0)
        {
            playerHealthData.health = playerHealthData.maxHealth;
        }
    }
}