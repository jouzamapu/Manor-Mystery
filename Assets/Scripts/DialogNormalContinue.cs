using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogNormalContinue : Dialog
{
    public Dialog nextDialogue;
    public string[] lines;
    public TextMeshProUGUI textComponent;
    private int index;
    public float textSpeed;
    public override void Initialize(DialogueManager dialogueManager)
    {
        base.Initialize(dialogueManager);
        textComponent.text = string.Empty;
        StartDialogue();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if (textComponent.text == lines[index])
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                textComponent.text = lines[index];
            }
        }
    }

    void StartDialogue()
    {
        index = 0;
        StartCoroutine(TypeLine());

    }

    IEnumerator TypeLine()
    {
        // type each character 1 by 1
        foreach (char c in lines[index].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            EndDialogue();
        }
    }

    public override void EndDialogue()
    {
        dialogueManager.AddDialogue(nextDialogue);
        base.EndDialogue();
    }
}
