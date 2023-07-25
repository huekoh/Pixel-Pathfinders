using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseDialogueTrigger : MonoBehaviour
{
    [Header("Ink JSON")]
    [SerializeField] private TextAsset inkJSON;
    [SerializeField] private ItemGiver itemGiver;
    [SerializeField] private PlayerControl player;
    [SerializeField] private Vector3 moveBackDistance;

    private void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.CompareTag("Player") && !itemGiver.hasGivenItem)
        {
            player.canMove = false;
            DialogueManager.GetInstance().EnterDialogueMode(inkJSON);
            StartCoroutine(MovePlayerBackCoroutine());
        }
    }

    private void OnTriggerExit2D(Collider2D col) {
    }

    private IEnumerator MovePlayerBackCoroutine()
    {
        while (DialogueManager.GetInstance().dialogueIsPlaying)
        {
            yield return null;
        }

        Vector3 newPosition = player.transform.position - moveBackDistance;
        player.transform.position = newPosition;

        yield return new WaitForSeconds(0.5f);
        player.canMove = true;
    }

    private void MovePlayerBack()
    {
        Vector3 newPosition = player.transform.position - moveBackDistance;
        player.transform.position = newPosition;
    }
}
