using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GardenPick : MonoBehaviour
{
    private bool isChoosing;
    bool isMoving;
    [SerializeField] RectTransform poison;
    [SerializeField] Vector2 picked;
    [SerializeField] float speed;
    [SerializeField] SpeechRecognition speechRecognition;

    private void Start()
    {
        string[] strings = new string[3];
        strings[0] = "Bush";
        strings[1] = "Tree";
        strings[2] = "Fountain";
        
        speechRecognition.CheckForPhrases(strings);
        speechRecognition.speechIndexRecognized += ButtonClicked;

    }

    public void ButtonClicked(int index)
    {
        //bush
        if(index == 0)
        {
            speechRecognition.StopSpeech();
            ChooseBush();
        }
        else
        //tree / Fountain
        if(index == 1)
        {
            ChooseTree();
            speechRecognition.Restart();
        }
        else
        {
            ChooseFountain();
            speechRecognition.Restart();
        }
    }

    private void Update()
    {
        if(isMoving)
        {
            poison.anchoredPosition = Vector2.MoveTowards(poison.anchoredPosition, picked, speed * Time.deltaTime);
            if(poison.anchoredPosition == picked)
            {
                isMoving = false;
                //LOAD SCENE dialog setelah dapet poison dari BUSH
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }
    }

    public void ChooseBush()
    {
        if(isChoosing) return;
        isChoosing = true;
        isMoving = true;
    }

    public void ChooseTree()
    {
        if(isChoosing) return;
        isChoosing = true;
        Debug.Log("dialog kalo salah milih");
        isChoosing = false;
    }

    public void ChooseFountain()
    {
        if(isChoosing) return;
        isChoosing = true;
        Debug.Log("dialog kalo salah milih");
        isChoosing = false;
    }
}
