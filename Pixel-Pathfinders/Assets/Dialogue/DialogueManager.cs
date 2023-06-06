using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Ink.Runtime;

public class DialogueManager : MonoBehaviour
{
    [Header("Dialogue UI")]

    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TextMeshProUGUI dialogueText;

    private Story currentStory;
    public bool dialogueIsPlaying { get; private set; }
    private static DialogueManager instance;

    private void Awake() {
        if (instance != null) {
            Debug.LogError("Found more than one Dialogue manager in the scene");
        }
        instance = this;
    }

    public static DialogueManager GetInstance() {
        return instance;
    }

    private void Start() {
        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);
    }

    private void Update() {

        // Return right away if dialogue is not playing
        if (!dialogueIsPlaying) {
            return;
        }

        // handle continuation of dialogue to next line if E is pressed
        if (Input.GetKeyDown(KeyCode.E)) {
            ContinueStory();
        }
    }

    public void EnterDialogueMode(TextAsset inkJSON) {
        currentStory = new Story(inkJSON.text);
        dialogueIsPlaying = true;
        dialoguePanel.SetActive(true);
    }

    private void ExitDialogueMode() {
        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);
        dialogueText.text = "";
    }

    private IEnumerator ExitDialogueCoroutine()
    {
        yield return new WaitForSeconds(0.01f);
        ExitDialogueMode();
    }

    private void ContinueStory() {
        if (currentStory.canContinue) {
            dialogueText.text = currentStory.Continue();
        } else {
            StartCoroutine(ExitDialogueCoroutine());
        }
    }
}
