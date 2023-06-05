using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour
{
    public float damage = 1;
    public float knockbackForce = 5f;
    public float moveSpeed= 5f;
    Animator animator;
    public DetectionZone detectionZone;
    Rigidbody2D rb;
    Collider2D playerCollider;
    public bool isCollidingWithPlayer = false;
    float damageTimer = 0f;
    // damageInterval set to be same as Invulnerability timer
    public float damageInterval = 0.5f;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        playerCollider = GameObject.FindGameObjectWithTag("Player").GetComponent<Collider2D>();
    }

    void FixedUpdate() {
        if (detectionZone.detectedObjects.Count > 0) {
            // Calculate direction to player
            Vector2 direction = (detectionZone.detectedObjects[0].transform.position - transform.position).normalized;
            // Move towards player
            rb.AddForce(direction * moveSpeed * Time.deltaTime);

            animator.SetFloat("Speed", moveSpeed);

            if (direction.x > 0) {
                animator.SetFloat("Direction.x", 1);
            } else if (direction.x < 0) {
                animator.SetFloat("Direction.x", -1);
            }
        } else {
            animator.SetFloat("Speed", 0f);
        }

        // While Slime is in contact with Player, run damageTimer to 0.5 seconds then deal damage again
        if (isCollidingWithPlayer) {
            damageTimer += Time.deltaTime;
            if (damageTimer > damageInterval) {
                    DealDamage();
                    damageTimer = 0f;
            }
        }
    }

    void OnCollisionEnter2D(Collision2D col) {
        if (col.gameObject.tag == "Player") {
            // On first collision, deal damage to Player
            DealDamage();
            // Set collision state to true
            isCollidingWithPlayer = true;
        }
    }

    void OnCollisionExit2D(Collision2D col) {
        if (col.gameObject.tag == "Player") {
            // Set collision state to false
            isCollidingWithPlayer = false;
        }
    }

    void DealDamage() {
        IDamageable damageable = playerCollider.GetComponent<IDamageable>();
            
            if (damageable != null) {
                
                Vector2 direction = (playerCollider.transform.position - transform.position);
                Vector2 knockback = direction * knockbackForce;

                damageable.OnHit(damage, knockback);
            }
    }
}
