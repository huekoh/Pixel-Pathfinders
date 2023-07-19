using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DamageableCharacter : MonoBehaviour, IDamageable
{
    Animator animator;
    Rigidbody2D rb;
    SpriteRenderer spriteRenderer;
    Color originalColor;
    //bool isAlive = true;
    bool isInvulnerable = false;
    public GameObject healthTextPrefab;
    public float health;
    public float maxHealth;

    [SerializeField] EnemyHealthBar healthBar;

    public void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        healthBar = GetComponentInChildren<EnemyHealthBar>();
    }

    public void Start() {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
        healthBar.UpdateHealthBar(health, maxHealth);
    }

    public void OnHit(float damage, Vector2 knockback)
    {
        if (!isInvulnerable) {
            health -= damage;
            healthBar.UpdateHealthBar(health, maxHealth);
            rb.AddForce(knockback);

            // Change color of character to red everytime it's hit
            spriteRenderer.color = Color.red;
            StartCoroutine(RestoreColorCoroutine());

            // If health <= 0, destroy character after 0.3 seconds
            if (health <= 0) {
                Invoke("DestroySelf", 0.3f);
            }

            // Show floating text
            if (healthTextPrefab) {
                ShowHealthText(damage);
            }

            // Set invulnerability to true after getting hit
            StartCoroutine(InvulnerabilityCoroutine());
            }
    }
    
    void ShowHealthText(float damage) {
        var text = Instantiate(healthTextPrefab, transform.position, Quaternion.identity, transform);
        text.GetComponent<TextMeshPro>().text = damage.ToString();
    }

    public void OnHit(float damage)
    {
        health -= damage;
    }

    public void DestroySelf() {
        Destroy(gameObject);
    }

    private IEnumerator RestoreColorCoroutine() {
        // Wait for 0.2 seconds
        yield return new WaitForSeconds(0.2f);
        // Restore the original color
        spriteRenderer.color = originalColor;
    }

    private IEnumerator InvulnerabilityCoroutine() {
        isInvulnerable = true;
        // Wait for 0.5 seconds
        yield return new WaitForSeconds(0.5f);
        // Reset the invulnerability flag
        isInvulnerable = false;
    }
}