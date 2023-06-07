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
    [SerializeField] private TextMeshProUGUI displayNameText;
    [SerializeField] private Animator portraitAnimator;

    private Story currentStory;
    public bool dialogueIsPlaying { get; private set; }
    private bool isTyping;
    private static DialogueManager instance;

    private const string SPEAKER_TAG = "speaker";
    private const string PORTRAIT_TAG = "portrait";
    private const string LAYOUT_TAG = "layout";

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
        isTyping = false;
        dialoguePanel.SetActive(false);
    }

    private void Update() {

        // Return right away if dialogue is not playing
        if (!dialogueIsPlaying) {
            return;
        }

        // handle continuation of dialogue to next line if E is pressed
        if (Input.GetKeyDown(KeyCode.E) && !isTyping) {
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

    private IEnumerator TypeText(string text) {
        dialogueText.text = "";
        isTyping = true;
        foreach (char c in text) {
            dialogueText.text += c;
            yield return new WaitForSeconds(0.03f);
        }
        isTyping = false;
    }

    private void ContinueStory() {
        if (currentStory.canContinue) {
            dialogueText.text = currentStory.Continue();
            StartCoroutine(TypeText(dialogueText.text));
            HandleTags(currentStory.currentTags);
        } else {
            StartCoroutine(ExitDialogueCoroutine());
        }
    }

    private void HandleTags(List<string> currentTags) {
        // Loop through and handle each tag
        foreach (string tag in currentTags) {
            // Parse the tag
            string[] splitTag = tag.Split(':');
            if (splitTag.Length != 2) {
                Debug.LogError(" Tag could not be appropriately parsed: " + tag);
            }
            string tagKey = splitTag[0].Trim();
            string tagValue = splitTag[1].Trim();

            // Handle the tag
            switch (tagKey) {
                case SPEAKER_TAG:
                    displayNameText.text = tagValue;
                    break;
                case PORTRAIT_TAG:
                    portraitAnimator.Play(tagValue);
                    break;
                case LAYOUT_TAG:
                    Debug.Log("speaker=" + tagValue);
                    break;
                default:
                    Debug.LogWarning("Tag came in but is not currently being handled: " + tag);
                    break;
            }
        }
    }
}
