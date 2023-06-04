using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageableCharacter : MonoBehaviour, IDamageable
{
    Animator animator;
    Rigidbody2D rb;
    SpriteRenderer spriteRenderer;
    Color originalColor;
    bool isAlive = true;
    public float Health {
        set {
            health = value;
        }
        get {
            return health;
        }
    }

    public float health = 5;

    public void Start() {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
    }

    public void OnHit(float damage, Vector2 knockback)
    {
        Health -= damage;
        rb.AddForce(knockback);
        // Change color of slime to red everytime it's hit
        spriteRenderer.color = Color.red;
        StartCoroutine(RestoreColorCoroutine());

        // If health <= 0, destroy slime after 0.3 seconds
        if (health <= 0) {
            Invoke("DestroySelf", 0.3f);
        }
    }

    public void OnHit(float damage)
    {
        Health -= damage;
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