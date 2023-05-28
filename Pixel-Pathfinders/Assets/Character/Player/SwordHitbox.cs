using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordHitbox : MonoBehaviour
{
    public Collider2D swordCollider;
    public float swordDamage = 1f;
    public Vector3 faceDown = new Vector3(-0.16f, -1.04f, 0);
    public Vector3 faceUp = new Vector3(-0.14f, 0.57f, 0);
    public Vector3 faceLeft = new Vector3(-1.04f, -0.34f, 0);
    public Vector3 faceRight = new Vector3(0.72f, -0.34f, 0);

    // Start is called before the first frame update
    void Start()
    {
        if (swordCollider == null) {
            Debug.Log("Sword Collider not set!");
        }
    }

    void OnTriggerEnter2D(Collider2D collider) {
        collider.SendMessage("OnHit", swordDamage);
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
    }
}
