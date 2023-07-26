using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Ink.Runtime;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    [Header("Dialogue UI")]

    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private TextMeshProUGUI displayNameText;
    [Header("Choices UI")]
    [SerializeField] private GameObject[] choices;
    private TextMeshProUGUI[] choicesText;
    [SerializeField] private Animator portraitAnimator;
    private Story currentStory;
    public bool dialogueIsPlaying { get; private set; }
    private bool isTyping;
    private bool choiceMade;
    public delegate void ChoiceMadeHandler(int choiceIndex);
    public static event ChoiceMadeHandler OnChoiceMade;
    private static DialogueManager instance;
    private const string SPEAKER_TAG = "speaker";
    private const string PORTRAIT_TAG = "portrait";
    private ShopUIManager shopUIManager;

    private void Awake() {
        if (instance != null) {
            Debug.LogError("Found more than one Dialogue manager in the scene");
        }
        instance = this;

        shopUIManager = FindObjectOfType<ShopUIManager>();
    }

    public static DialogueManager GetInstance() {
        return instance;
    }

    private void Start() {
        dialogueIsPlaying = false;
        isTyping = false;
        choiceMade = false;
        dialoguePanel.SetActive(false);

        choicesText = new TextMeshProUGUI[choices.Length];
        int index = 0;
        foreach (GameObject choice in choices) 
        {
            choicesText[index] = choice.GetComponentInChildren<TextMeshProUGUI>();
            index++;
        }

        for (int i = 0; i < choices.Length; i++)
        {
            choices[i].gameObject.SetActive(false);
        }
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
        EventSystem.current.SetSelectedGameObject(null);
        ContinueStory();
        if (shopUIManager != null)
        {
            shopUIManager.dialogueStarted = true;
        }
    }

    private void ExitDialogueMode() {
        choiceMade = false;
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
        if (currentStory.currentChoices.Count > 0) {
        // If choices are available, do not continue the story.
            return;
        }

        if (currentStory.canContinue) {
            dialogueText.text = currentStory.Continue();
            StartCoroutine(TypeText(dialogueText.text));
            StartCoroutine(ShowChoicesAfterText());
            HandleTags(currentStory.currentTags);
        } else {
            StartCoroutine(ExitDialogueCoroutine());
        }
    }

    private void DisplayChoices()
    {
        List<Choice> currentChoices = currentStory.currentChoices;

        // defensive check to make sure our UI can support the number of choices coming in
        if (currentChoices.Count > choices.Length)
        {
            Debug.LogError("More choices were given than the UI can support. Number of choices given: " 
                + currentChoices.Count);
        }

        int index = 0;
        // enable and initialize the choices up to the amount of choices for this line of dialogue
        foreach(Choice choice in currentChoices) 
        {
            choices[index].gameObject.SetActive(true);
            choicesText[index].text = choice.text;

            index++;
        }
        // go through the remaining choices the UI supports and make sure they're hidden
        for (int i = index; i < choices.Length; i++) 
        {
            choices[i].gameObject.SetActive(false);
        }
    }

    private IEnumerator ShowChoicesAfterText()
    {
        // Wait until the text typing coroutine finishes before showing choices
        while (isTyping)
        {
            yield return null;
        }

        DisplayChoices(); // Show the choices after text typing is complete
    }

    public void MakeChoice(int choiceIndex)
    {
        if (choiceMade)
        {
            return;
        }

        currentStory.ChooseChoiceIndex(choiceIndex);

        for (int i = 0; i < choices.Length; i++)
        {
            choices[i].gameObject.SetActive(false);
        }

        if (OnChoiceMade != null)
        {
            OnChoiceMade(choiceIndex);
        }

        choiceMade = true;

        ContinueStory();
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
                default:
                    Debug.LogWarning("Tag came in but is not currently being handled: " + tag);
                    break;
            }
        }
    }
}