using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Windows.Speech;
using UnityEngine.UI;

public class SpeechMovementControl : MonoBehaviour
{
    private KeywordRecognizer keywordRecognizer;
    private Dictionary<string, Action> actions = new Dictionary<string, Action>();
    [SerializeField]private Button btn;

    void Start()
    {
        actions.Add("upward", Up);
        actions.Add("down", Down);
        actions.Add("right", Right);
        actions.Add("left", Left);

        keywordRecognizer = new KeywordRecognizer(actions.Keys.ToArray());
        keywordRecognizer.OnPhraseRecognized += RecognizedSpeech;
        //keywordRecognizer.Start(); //nanti taro di button, pas mau mulai ngomong
    }

    private void RecognizedSpeech(PhraseRecognizedEventArgs speech)
    {
        Debug.Log(speech.text);
        actions[speech.text].Invoke();
    }

    private void Up()
    {
        transform.Translate(0, 1, 0);
    }

    private void Down()
    {
        transform.Translate(0, -1, 0);
    }

    private void Right()
    {
        transform.Translate(1, 0, 0);
    }

    private void Left()
    {
        transform.Translate(-1, 0, 0);
    }
}