using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Windows.Speech;
using UnityEngine.UI;

public class GridPuzzleSpeech : MonoBehaviour
{
    private KeywordRecognizer keywordRecognizer;
    private Dictionary<string, Action> actions = new Dictionary<string, Action>();
    private int index;
    [SerializeField]private Button btn;
    [SerializeField] private string[] dialogues;
    private string speechedText;
    private bool CorrectAnswer = false;
    [SerializeField] private GridPuzzle gridPuzzle;

    void Start()
    {
        actions.Add("up", gridPuzzle.MoveUp);
        actions.Add("down", gridPuzzle.MoveDown);
        actions.Add("right", gridPuzzle.MoveRight);
        actions.Add("left", gridPuzzle.MoveLeft);
        // for(int i = 0;i < dialogues.Length;i++) actions.Add(dialogues[i],check1);

        keywordRecognizer = new KeywordRecognizer(actions.Keys.ToArray());
        keywordRecognizer.OnPhraseRecognized += RecognizedSpeech;
        // keywordRecognizer.Start(); //nanti taro di button, pas mau mulai ngomong

        if(gridPuzzle.useSpeechRecog)
        {
            StartSpeech();
        }
    }

    public void StartSpeech() => keywordRecognizer.Start();

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


}
