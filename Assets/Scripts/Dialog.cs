using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

[System.Serializable]
public class Dialog : MonoBehaviour
{
    public UnityEvent OnDialogFinish;
    protected DialogueManager dialogueManager;
    public delegate void DialogEnd(Dialog dialog);
    public DialogEnd OnDialogEnd;

    // Start is called before the first frame update
    public virtual void Initialize(DialogueManager dialogueManager)
    {
        this.dialogueManager = dialogueManager;
    }

    public virtual void EndDialogue()
    {
        if (OnDialogFinish != null) 
        {
            OnDialogFinish.Invoke();
        }

        if (OnDialogEnd != null) 
        {
            OnDialogEnd.Invoke(this);
        }
        Destroy(this.gameObject);
    }
}
