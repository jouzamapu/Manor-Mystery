using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button PlayButton;
    [SerializeField] private Button OptionButton;
    [SerializeField] private Button ExitButton;
    [SerializeField] private GameObject LevelSelection;
    [SerializeField] private GameObject MainMenuRoot;
    [SerializeField] private SpeechRecognition speechRecognizer;
    [SerializeField] private SceneManager sceneManager;

    private void Start()
    {
        AudioManager.instance.Stop("RainyAmbience");
        AudioManager.instance.Play("MenuBGM");

        string[] strings= new string[2];
        strings[0] = "Play";
        strings[1] = "Exit";    

        speechRecognizer.CheckForPhrases(strings);
        speechRecognizer.speechIndexRecognized += ButtonClicked;
 
    }

    private void ButtonClicked(int index)
    {
        //play
        if(index == 0)
        {
            GoToNextScene();
        }
        //exit
        else if(index == 1)
        {
            QuitGame();
        }
    }

    public void LoadSceneFunction(string SceneName)
    {
        SceneManager.LoadScene(SceneName);
    }

    public void OpenableSelectionMenu()
    {
        MainMenuRoot.SetActive(false);
        LevelSelection.SetActive(true);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void GoToNextScene()
    {
        Debug.Log("next");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
