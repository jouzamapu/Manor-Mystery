using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagers : MonoBehaviour
{
    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void GoToNextScene()
    {
        Debug.Log("next");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
