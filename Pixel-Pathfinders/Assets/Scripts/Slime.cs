using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour, IDamageable
{
    Animator animator;
    Rigidbody2D rb;
    bool isAlive = true;
    public float Health {
        set {
            health = value;
        }
        get {
            return health;
        }
    }

    public float health = 3;

    public void Start() {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    public void OnHit(float damage, Vector2 knockback)
    {
        Health -= damage;
        rb.AddForce(knockback);
       // Debug.Log("Force " + knockback);
    }

    public void OnHit(float damage)
    {
        Health -= damage;
    }
}
