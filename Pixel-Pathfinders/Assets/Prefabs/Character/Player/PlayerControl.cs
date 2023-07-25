using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
//using UnityEngine.EventSystems;

public class PlayerControl : MonoBehaviour
{
    public GameObject swordHitbox;
    private Collider2D swordCollider;
    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private Vector2 movement;
    private GameObject shop;
    private bool canMove = true;
    public VectorValue startingPosition;
    public InventoryObject playerEquipment;
    private float moveSpeed = 5f;
    public bool isAttacking = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        swordCollider = swordHitbox.GetComponent<Collider2D>();
        transform.position = startingPosition.initialValue;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerEquipment.Container.Slots[1].item.Id >= 0)
        {
            animator.SetBool("isHoldingShield", true);
        } else {
            animator.SetBool("isHoldingShield", false);
        }

        if (DialogueManager.GetInstance().dialogueIsPlaying)
        {
            animator.SetFloat("Speed", 0);
            moveSpeed = 0f;
        }
        else
        {
            moveSpeed = 5f;
        }
        
        if (canMove == true && !DialogueManager.GetInstance().dialogueIsPlaying)
        {
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

    void FixedUpdate()
    {
        if (canMove == true) {
            rb.MovePosition(rb.position + movement.normalized * moveSpeed * Time.fixedDeltaTime);
        }
    }

    void OnFire()
    {
        if (playerEquipment.Container.Slots[0].item.Id == -1)
        {
            Debug.Log("No Sword is equipped!");
            return;
        }
        if (EventSystem.current.IsPointerOverGameObject())
            return;
            
        if (!DialogueManager.GetInstance().dialogueIsPlaying)
        {
            animator.SetTrigger("swordAttack");
        }
    }

    public void LockMovement()
    {
        canMove = false;
    }

    public void UnlockMovement()
    {
        canMove = true;
    }

    public void IsAttacking()
    {
        isAttacking = true;
    }

    public void IsNotAttacking()
    {
        isAttacking = false;
    }
}
