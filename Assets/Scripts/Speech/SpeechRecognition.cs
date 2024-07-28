using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Windows.Speech;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SpeechRecognition : MonoBehaviour
{

    public delegate void recognizedEvent(int index);
    public recognizedEvent speechIndexRecognized;
    private KeywordRecognizer keywordRecognizer;
    private Dictionary<string, Action> actions = new Dictionary<string, Action>();
    private int index;
    [SerializeField] private List<String> dialogues;
    private string speechedText;
    private bool CorrectAnswer = false;

    void Start()
    {
        // for(int i = 0; i < dialogues.Length; i++) actions.Add(dialogues[i],check1);

        // keywordRecognizer = new KeywordRecognizer(actions.Keys.ToArray());
        // keywordRecognizer.OnPhraseRecognized += RecognizedSpeech;
        //keywordRecognizer.Start(); //nanti taro di button, pas mau mulai ngomong
    }

    public void CheckForPhrases(string [] keywords)
    {
        //reset
        dialogues.Clear();
        dialogues = new List<string>();
        keywordRecognizer = null;

        //add new keywords
        for(int i = 0 ; i < keywords.Length ; i ++)
        {
            dialogues.Add(keywords[i]);
        }

        //initiate keyword recognizer
        keywordRecognizer = new KeywordRecognizer(keywords);
        keywordRecognizer.OnPhraseRecognized += RecognizedSpeech;
        keywordRecognizer.Start();
    }

    public void Restart()
    {
        keywordRecognizer.Start();
    }

    public void StartSpeech() => StartCoroutine(Starts());
    public void StopSpeech() => keywordRecognizer.Stop();

    IEnumerator Starts()
    {   
        keywordRecognizer.Start();

        yield return new WaitForSeconds(5.0f);

        keywordRecognizer.Stop();
    }

    private void RecognizedSpeech(PhraseRecognizedEventArgs speech)
    {
        speechedText = speech.text;

        Debug.Log(speech.text);

        CheckOption();
    }

    private void CheckOption()
    {
        for(int i = 0; i < dialogues.Count; i++)
        {
            if(speechedText == dialogues[i])
            {
                keywordRecognizer.Stop();

                //option picked
                if(speechIndexRecognized != null)
                {
                    speechIndexRecognized.Invoke(i);
                }

                break;
            }
        }
    }

    public void ChangeScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
