using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamageableObject : MonoBehaviour, IDamageable
{
    Rigidbody2D rb;
    SpriteRenderer spriteRenderer;
    Color originalColor;
    bool isInvulnerable = false;
    public GameObject healthTextPrefab;
    public float health;
    public float maxHealth;
    private ObjectHealthBar healthBar;

    public void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        healthBar = GetComponentInChildren<ObjectHealthBar>();
    }

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
    }

    public void OnHit(float damage, Vector2 knockback)
    {
        if (!isInvulnerable)
        {
            health -= damage;
            healthBar.UpdateHealthBar(health, maxHealth);
    

            rb.AddForce(knockback);

            //change colour of object to red everytime it's hit
            spriteRenderer.color = Color.red;
            StartCoroutine(RestoreColorCoroutine());

            if (health <= 0)
            {
                Invoke("DestroySelf", 0.3f);
            }

            if (healthTextPrefab)
            {
                ShowHealthText(damage);
            }

            StartCoroutine(InvulnerabilityCoroutine());
        }
    }

    void ShowHealthText(float damage)
    {
        var text = Instantiate(healthTextPrefab, transform.position, Quaternion.identity, transform);
        text.GetComponent<TextMeshPro>().text = damage.ToString();
    }


    public void DestroySelf()
    {
        Destroy(gameObject);
    }

    private IEnumerator RestoreColorCoroutine()
    {
        //wait for 0.2 seconds
        yield return new WaitForSeconds(0.2f);
        //restore the original color
        spriteRenderer.color = originalColor;
    }

    private IEnumerator InvulnerabilityCoroutine()
    {
        isInvulnerable = true;
        //Wait for 0.5 seconds
        yield return new WaitForSeconds(0.5f);
        //Reset invulnerability flag
        isInvulnerable = false;
    }
}
