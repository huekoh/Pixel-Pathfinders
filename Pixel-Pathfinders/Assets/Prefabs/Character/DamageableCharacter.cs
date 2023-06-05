using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamageableCharacter : MonoBehaviour, IDamageable
{
    Animator animator;
    Rigidbody2D rb;
    SpriteRenderer spriteRenderer;
    Color originalColor;
    bool isAlive = true;
    public GameObject healthTextPrefab;
    public float health = 5;

    public void Start() {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
    }

    public void OnHit(float damage, Vector2 knockback)
    {
        health -= damage;
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
}