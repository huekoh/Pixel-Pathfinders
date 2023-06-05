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

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
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
    }

    void OnCollisionEnter2D(Collision2D col) {
        if (col.gameObject.tag == "Player") {

            IDamageable damageable = col.collider.GetComponent<IDamageable>();
            
            if (damageable != null) {
                
                Vector2 direction = (col.collider.transform.position - transform.position);
                Vector2 knockback = direction * knockbackForce;

                damageable.OnHit(damage, knockback);
            }
        }
    }
}
