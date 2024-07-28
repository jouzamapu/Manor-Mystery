using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DialogEnd : Dialog
{
    public float duration;
    private float currentDuration;

    public override void Initialize(DialogueManager dialogueManager)
    {
        base.Initialize(dialogueManager);
        currentDuration = duration;
    }

    void Update()
    {
        if(duration > 0)
        {
            duration -= Time.deltaTime;
        }
        else
        {
            EndDialogue();
        }
    }

    public override void EndDialogue()
    {
        Debug.Log("end");
        OnDialogFinish?.Invoke();
        // if (OnDialogFinish != null) 
        // {
        //     OnDialogFinish.Invoke();
        // }
        
        // if (OnDialogEnd != null) 
        // {
        //     OnDialogEnd.Invoke(this);
        // }
        // if(SceneManager.GetActiveScene().buildIndex == 0 || SceneManager.GetActiveScene().buildIndex == 4) //GANTI INDEX TERAKHIR
        // {
        //     SceneManager.LoadScene("MainMenu");
        // }
    }
}
