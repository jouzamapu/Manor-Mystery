using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Windows.Speech;
using UnityEngine.UI;

public class SpeechMovementTest : MonoBehaviour
{
    private KeywordRecognizer keywordRecognizer;
    private Dictionary<string, Action> actions = new Dictionary<string, Action>();
    private int index;
    [SerializeField]private Button btn;
    [SerializeField] private string[] dialogues;
    private string speechedText;
    private bool CorrectAnswer = false;

    void Start()
    {
        actions.Add("up", Up);
        actions.Add("down", Down);
        actions.Add("right", Right);
        actions.Add("left", Left);
        for(int i = 0;i < dialogues.Length;i++) actions.Add(dialogues[i],check1);

        keywordRecognizer = new KeywordRecognizer(actions.Keys.ToArray());
        keywordRecognizer.OnPhraseRecognized += RecognizedSpeech;
        //keywordRecognizer.Start(); //nanti taro di button, pas mau mulai ngomong
    }

    public void StartSpeech() => StartCoroutine(Starts());

    IEnumerator Starts()
    {   
        keywordRecognizer.Start();
        
        btn.interactable = false;
        Debug.Log("start");

        yield return new WaitForSeconds(5.0f);

        keywordRecognizer.Stop();
        btn.interactable = true;
        Debug.Log("stop");
        if(CorrectAnswer == false) Debug.Log("gagal");
        if(CorrectAnswer == true) CorrectAnswer = false;
    }

    public void StopSpeech() => keywordRecognizer.Stop();

    private void RecognizedSpeech(PhraseRecognizedEventArgs speech)
    {
        Debug.Log(speech.text);
        speechedText = speech.text;
        actions[speech.text].Invoke();
    }

    private void check1()
    {
        for(int i = index; i < dialogues.Length; i++)
        {
            if(i == index && speechedText == dialogues[i]) 
            {
                Debug.Log("Berhasil Kalimat ke - " + (i+1));
                index++;
                CorrectAnswer = true;
                keywordRecognizer.Stop();
                btn.interactable = true;
                StopAllCoroutines();
                if(CorrectAnswer == true) CorrectAnswer = false;
            }
            else
            {
                keywordRecognizer.Stop();
                btn.interactable = true;
                StopAllCoroutines();
                if(CorrectAnswer == false) Debug.Log("gagal");
                break;
            }
        }
    }

    private void Up()
    {
        transform.Translate(0, 1, 0);
        CorrectAnswer = true;
    }

    private void Down()
    {
        transform.Translate(0, -1, 0);
        CorrectAnswer = true;
    }

    private void Right()
    {
        transform.Translate(1, 0, 0);
        CorrectAnswer = true;
    }

    private void Left()
    {
        transform.Translate(-1, 0, 0);
        CorrectAnswer = true;
    }
}