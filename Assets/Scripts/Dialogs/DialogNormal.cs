using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;

public class DialogNormal : Dialog
{
    public enum CharacterAppearance
    {
        appear,
        dissappear,
        none
    }

    public Transform CharacterRoot;
    public GameObject CharacterPrefab;
    public DialogBlock[] dialogBlocks;
    public Dictionary<string,GameObject> Speakers;
    public TextMeshProUGUI textComponent;
    public TextMeshProUGUI speakerNameText;
    private int index;
    public float textSpeed;
    private bool initialize = false;

    public override void Initialize(DialogueManager dialogueManager)
    {
        base.Initialize(dialogueManager);
        textComponent.text = string.Empty;

        //add all the characters
        Speakers = new Dictionary<string, GameObject>();
        List<SO_Characters> dialogCharacters = new List<SO_Characters>();
        for(int i = 0 ; i < dialogBlocks.Length ; i ++)
        {
            if(!dialogCharacters.Contains(dialogBlocks[i].speaker))
            {
                dialogCharacters.Add(dialogBlocks[i].speaker);

                GameObject temp = Instantiate(CharacterPrefab,CharacterRoot);
                UnityEngine.UI.Image image = temp.GetComponent<UnityEngine.UI.Image>();
                image.sprite = dialogBlocks[i].speaker.CharacterSprite;
                Speakers.Add(dialogBlocks[i].speaker.CharacterName,temp);

                if(dialogBlocks[i].ShowOnStart)
                {
                    temp.SetActive(true);
                }
                else
                {
                    temp.SetActive(false);
                }
                
            }
        }

        //speech recognition
        string[] strings = new string[1];
        strings[0] = "next";

        dialogueManager.SpeechRecognition.CheckForPhrases(strings);
        dialogueManager.SpeechRecognition.speechIndexRecognized += ButtonClicked;
 
        initialize = true;
        StartDialogue();
    }

    // Update is called once per frame
    void Update()
    {
        if(initialize)
        {
            if(Input.GetMouseButtonDown(0))
            {
                // Debug.Log(index + "\\" + dialogBlocks.Length);
                if (textComponent.text != null && textComponent.text == dialogBlocks[index].text)
                {
                    NextLine();
                }
                else if (index > dialogBlocks.Length - 1)
                {
                    NextLine();
                }
                else
                {
                    StopAllCoroutines();
                    textComponent.text = dialogBlocks[index].text;
                }
            }
        }
    }

    public void ButtonClicked(int empty)
    {
                // Debug.Log(index + "\\" + dialogBlocks.Length);
                if (textComponent.text != null && textComponent.text == dialogBlocks[index].text)
                {
                    NextLine();
                }
                else if (index > dialogBlocks.Length - 1)
                {
                    NextLine();
                }
                else
                {
                    StopAllCoroutines();
                    textComponent.text = dialogBlocks[index].text;
                }
                    dialogueManager.SpeechRecognition.Restart();

    }

    void StartDialogue()
    {
        index = 0;

        if(dialogBlocks[index].AudioName != null) AudioManager.instance.Play(dialogBlocks[index].AudioName);

        ChangeCharacter();

        StartCoroutine(TypeLine());

    }

    IEnumerator TypeLine()
    {
        // type each character 1 by 1
        foreach (char c in dialogBlocks[index].text.ToCharArray())
        {
            textComponent.text += c;

            AudioManager.instance.Play("TypeUI");

            yield return new WaitForSeconds(textSpeed);
        }
        AudioManager.instance.Stop("TypeUI");
    }

    void NextLine()
    {
        if (index < dialogBlocks.Length - 1)
        {
            AudioManager.instance.Stop(dialogBlocks[index].AudioName);
            index++;

            AudioManager.instance.Play(dialogBlocks[index].AudioName);

            ChangeCharacter();

            //light up the speaking character

            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            dialogueManager.SpeechRecognition.speechIndexRecognized -= ButtonClicked;
            dialogueManager.SpeechRecognition.StopSpeech();
            EndDialogue();
        }
    }

    public void ChangeCharacter()
    {
    
            if(dialogBlocks[index].appearance == CharacterAppearance.appear)
            {
                Speakers[dialogBlocks[index].speaker.CharacterName].SetActive(true);
            }
            else if (dialogBlocks[index].appearance == CharacterAppearance.dissappear)
            {
                Speakers[dialogBlocks[index].speaker.CharacterName].SetActive(false);
            }

            speakerNameText.text = dialogBlocks[index].speaker.CharacterName;
    }
    // public void GoToMenu()
    // {
    //     SceneManager.LoadScene("MainMenu");
    // }

}

[System.Serializable]
public class DialogBlock 
{

    public DialogNormal.CharacterAppearance appearance = DialogNormal.CharacterAppearance.none;
    public bool ShowOnStart = false;
    public SO_Characters speaker;
    public string text;

    public string AudioName;
}