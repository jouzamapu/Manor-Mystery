using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeypadPuzzle : MonoBehaviour
{
    private string ButtonNumbersInput;
    [SerializeField] private string Answer;
    [SerializeField] private SceneManagers SM;

    private void Start() 
    {
        ButtonNumbersInput = "";
    }

    public void NumberInput(string number)
    {
        if(ButtonNumbersInput.Length == Answer.Length) return;

        ButtonNumbersInput += number;
        AudioManager.instance.Play("KeypadInput");

        if(ButtonNumbersInput.Length == Answer.Length) NumberInputCheck();
    }

    public void NumberInputCheck()
    {
        if(Answer == ButtonNumbersInput)
        {
            AudioManager.instance.Play("SafeOpen");
            StartCoroutine(Delay());
        }
        else
        {
            AudioManager.instance.Play("SafeWrong");
            ButtonNumbersInput = "";
        }
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(2f);
        SM.GoToNextScene();
    }
}
