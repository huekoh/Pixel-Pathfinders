using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public float moveSpeed;
    public GameObject swordHitbox;
    Collider2D swordCollider;
    Rigidbody2D rb;
    Animator animator;
    SpriteRenderer spriteRenderer;
    private Vector2 movement;
    bool canMove = true;
    public VectorValue startingPosition;

    private static bool playerExists;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        swordCollider = swordHitbox.GetComponent<Collider2D>();
        transform.position = startingPosition.initialValue;
    }

    // Update is called once per frame
    void Update()
    {
        if (DialogueManager.GetInstance().dialogueIsPlaying) {
            animator.SetFloat("Speed", 0);
        }
        
        if (canMove == true && !DialogueManager.GetInstance().dialogueIsPlaying) {
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");

            animator.SetFloat("Horizontal", movement.x);
            animator.SetFloat("Vertical", movement.y);
            animator.SetFloat("Speed", movement.sqrMagnitude);

            if (Input.GetAxisRaw("Horizontal") == 1 || Input.GetAxisRaw("Horizontal") == -1 || Input.GetAxisRaw("Vertical") == 1 || Input.GetAxisRaw("Vertical") == -1) {
                animator.SetFloat("lastMoveX", Input.GetAxisRaw("Horizontal"));
                animator.SetFloat("lastMoveY", Input.GetAxisRaw("Vertical"));
                gameObject.BroadcastMessage("setDirection", new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")));
            }
        }
    }

    void FixedUpdate() {
        if (canMove == true && !DialogueManager.GetInstance().dialogueIsPlaying) {
            rb.MovePosition(rb.position + movement.normalized * moveSpeed * Time.fixedDeltaTime);
        }
    }

    void OnFire() {
        if (!DialogueManager.GetInstance().dialogueIsPlaying) {
            animator.SetTrigger("swordAttack");
        }
    }

    void LockMovement() {
        canMove = false;
    }

    void UnlockMovement() {
        canMove = true;
    }
}
