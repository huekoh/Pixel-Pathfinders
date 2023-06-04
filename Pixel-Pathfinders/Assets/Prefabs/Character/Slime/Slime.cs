using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour
{
    public float damage = 1;
    public float knockbackForce = 5f;
    public float moveSpeed= 5f;
    public DetectionZone detectionZone;
    Rigidbody2D rb;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate() {
        if (detectionZone.detectedObjects.Count > 0) {
            // Calculate direction to player
            Vector2 direction = (detectionZone.detectedObjects[0].transform.position - transform.position).normalized;

            // Move towards player
            rb.AddForce(direction * moveSpeed * Time.deltaTime);
            //Debug.Log("FORCE:" + direction * moveSpeed * Time.deltaTime);
        }
    }

    void OnCollisionEnter2D(Collision2D col) {
        if (col.gameObject.tag == "Player") {
            IDamageable damageable = col.collider.GetComponent<IDamageable>();
            if (damageable != null) {
                Vector2 direction = (col.collider.transform.position - transform.position);
            // Debug.Log("collider pos: " + collider.transform.position + " parentPos: " + transform.parent.position);
                Vector2 knockback = direction * knockbackForce;

                damageable.OnHit(damage, knockback);
            }
        }
    }
}
