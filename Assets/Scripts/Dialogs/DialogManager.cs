using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;
    void Awake()
    {
        if(instance == null) instance = this;
    }
    [SerializeField]
    private List<Dialog> DialogQueue;

    [SerializeField]
    private SpeechRecognition speechRecognition;
    public SpeechRecognition SpeechRecognition => speechRecognition;
    private int index;
    private int count;

    void Start()
    {
        count = DialogQueue.Count;

        if(SceneManager.GetActiveScene().buildIndex == 2)
        {
            AudioManager.instance.Stop("MenuBGM");
            AudioManager.instance.Play("LibraryBGM");
        }
        if(SceneManager.GetActiveScene().buildIndex == 5)
        {
            AudioManager.instance.Stop("LibraryBGM");
            AudioManager.instance.Play("GardenAmbience");
        }
        if(SceneManager.GetActiveScene().buildIndex == 11)
        {
            AudioManager.instance.Stop("GardenAmbience");
            AudioManager.instance.Play("LibraryBGM");
        }
        // if(SceneManager.GetActiveScene().buildIndex == 2)
        // {
        //     AudioManager.instance.Stop("MenuBGM");
        //     AudioManager.instance.Play("LibraryBGM");
        // }
        StartDialogue();
    }

    public void StartDialogue()
    {
        if(DialogQueue[0] != null)
        {
        Dialog temp = Instantiate<Dialog>(DialogQueue[0], transform);

        temp.Initialize(this);
        temp.OnDialogEnd += OnDialogEnd;
        }
    }

    public void AddDialogue(Dialog dialog)
    {
        DialogQueue.Add(dialog);
    }

    private void OnDialogEnd(Dialog dialog)
    {
        // index++;
        DialogQueue.RemoveAt(0);
        dialog.OnDialogEnd -= OnDialogEnd;

        StartDialogue();
        // if(index < count)
        // {
        //     StartDialogue();
        // }
    }

    /*

    public Text dialogueText;          // UI Text element to show the dialogue.
    public Button[] choiceButtons;     // Buttons to display choices.
    private Queue<string> sentences;   // Queue to manage the dialogue flow.

    public void StartDialogue(DialogTest2 dialogue)
    {
        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
        ShowChoices(false);  // Hide choices initially.
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        dialogueText.text = sentence;
    }

    private void EndDialogue()
    {
        ShowChoices(true);  // Show choices at the end of the dialogue.
    }

    private void ShowChoices(bool show)
    {
        foreach (Button button in choiceButtons)
        {
            button.gameObject.SetActive(show);
        }
    }*/
}
