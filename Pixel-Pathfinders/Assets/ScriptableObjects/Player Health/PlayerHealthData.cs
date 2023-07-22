using UnityEngine;

[CreateAssetMenu(fileName = "PlayerHealthData", menuName = "Player Health/PlayerHealthData", order = 1)]
public class PlayerHealthData : ScriptableObject
{
    public float health;
    public float maxHealth;
}