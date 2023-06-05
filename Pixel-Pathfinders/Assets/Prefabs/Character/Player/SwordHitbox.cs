using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordHitbox : MonoBehaviour
{
    public Collider2D swordCollider;
    public float swordDamage = 1f;
    public float knockbackForce = 5f;
    public Vector3 faceDown = new Vector3(0, -0.38f, 0);
    public Vector3 faceUp = new Vector3(0, 0.38f, 0);
    public Vector3 faceLeft = new Vector3(-0.38f, 0, 0);
    public Vector3 faceRight = new Vector3(0.38f, 0, 0);

    // Start is called before the first frame update
    void Start()
    {
        if (swordCollider == null) {
            Debug.Log("Sword Collider not set!");
        }
    }

    void setDirection(Vector2 direction) {
        if (direction.y == -1) {
            gameObject.transform.localPosition = faceDown;
        } else if (direction.y == 1) {
            gameObject.transform.localPosition = faceUp;
        } else if (direction.x == 1) {
            gameObject.transform.localPosition = faceRight;
        } else if (direction.x == -1) {
            gameObject.transform.localPosition = faceLeft;
        }
        //Debug.Log("Collision pos: " + gameObject.transform.localPosition);
    }

    void OnTriggerEnter2D(Collider2D collider) {

        IDamageable damageableObject = collider.GetComponent<IDamageable>();

        if (damageableObject != null) {
            
            Vector2 direction = (collider.transform.position - transform.parent.position);
            Vector2 knockback = direction * knockbackForce;

            damageableObject.OnHit(swordDamage, knockback);
        }
    }
}
