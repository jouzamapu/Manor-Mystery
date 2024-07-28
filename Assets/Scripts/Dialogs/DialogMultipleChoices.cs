using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using System;

public class DialogMultipleChoices : Dialog
{
    public List<TextMeshProUGUI> ButtonText; 
    public List<DialogChoice> DialogList;

    public override void Initialize(DialogueManager dialogueManager)
    {
        base.Initialize(dialogueManager);

        for (int i=0; i<DialogList.Count; i++)
        {
            ButtonText[i].text = DialogList[i].text;
        }

        //get the string list
        string[] strings = new string[DialogList.Count];

        for(int i = 0 ; i < DialogList.Count ; i ++)
        {
            strings[i] = DialogList[i].text;
        }

        dialogueManager.SpeechRecognition.CheckForPhrases(strings);
        dialogueManager.SpeechRecognition.speechIndexRecognized += ButtonClicked;
    }

    public void ButtonClicked(int index)
    {
        for (int i=0; i<DialogList[index].outcome.Count; i++)
        {
            dialogueManager.AddDialogue(DialogList[index].outcome[i]);
        }

        dialogueManager.SpeechRecognition.speechIndexRecognized -= ButtonClicked;
        
        Debug.Log(index);
        
        EndDialogue();
        // DialogueManager.instance.StartDialogue();
    }
}

[System.Serializable]
public class DialogChoice
{
    public string text;
    public List<Dialog> outcome;
}
