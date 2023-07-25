using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopUIManager : MonoBehaviour
{
    [Header("Shop UI")]

    [SerializeField] private GameObject shop;
    [SerializeField] private PlayerControl player;
    public bool dialogueStarted;

    // Start is called before the first frame update
    void Start()
    {
        shop.SetActive(false);
        dialogueStarted = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (dialogueStarted && !DialogueManager.GetInstance().dialogueIsPlaying)
        {
            shop.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            shop.SetActive(false);
            dialogueStarted = false;
        }

        if (shop.activeInHierarchy)
        {
            player.LockMovement();
        }
        else if (!shop.activeInHierarchy && !player.isAttacking)
        {
            player.UnlockMovement();
        }
    }
}