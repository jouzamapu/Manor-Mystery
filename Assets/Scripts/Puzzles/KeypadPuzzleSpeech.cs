using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Windows.Speech;
using UnityEngine.UI;

public class KeypadPuzzleSpeech : MonoBehaviour
{
    private KeywordRecognizer keywordRecognizer;
    private Dictionary<string, Action> actions = new Dictionary<string, Action>();
    private string speechedText;
    private bool CorrectAnswer = false;
    private bool isFinish = false;
    [SerializeField] private SceneManagers SM;

    void Start()
    {
        actions.Add("0512", Passcode);

        keywordRecognizer = new KeywordRecognizer(actions.Keys.ToArray());
        keywordRecognizer.OnPhraseRecognized += RecognizedSpeech;

        StartSpeech();
    }

    //Nanti dipanggil aj pas setelah next terakhir
    public void StartSpeech() => StartCoroutine(Starts());

    IEnumerator Starts()
    {   
        keywordRecognizer.Start();
        
        Debug.Log("start");

        //5.0f itu delayny nanti atur sesuai keperluan aj
        yield return new WaitForSeconds(10.0f);

        keywordRecognizer.Stop();
        Debug.Log("stop");
        if(CorrectAnswer == false) AudioManager.instance.Play("SafeWrong");
        if(CorrectAnswer == true) CorrectAnswer = false;

        StartSpeech();
    }

    private void RecognizedSpeech(PhraseRecognizedEventArgs speech)
    {
        Debug.Log(speech.text);
        speechedText = speech.text;
        actions[speech.text].Invoke();

        // StopAllCoroutines();
        keywordRecognizer.Stop();
    }

    private void Passcode()
    {
        Debug.Log("Passcode Benar");
        CorrectAnswer = true;

        AudioManager.instance.Play("SafeOpen");
        StartCoroutine(Delay());
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(2f);
        SM.GoToNextScene();
    }
}
