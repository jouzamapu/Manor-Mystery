using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;

public enum Movement
{
    UP, DOWN, LEFT, RIGHT
}
public class GridPuzzle : MonoBehaviour
{
    [SerializeField] Vector2[] positionArray;
    [SerializeField] int positionStart;
    int positionNow;
    bool isMoving;
    [SerializeField] float speed;
    [SerializeField] RectTransform playerPosition;
    [SerializeField] Movement[] correctOrder;
    int currentOrder = 0;
    Movement currentMovement;
    bool isChecking;
    [SerializeField] public bool useSpeechRecog;
    [SerializeField] private GridPuzzleSpeech gridPuzzleSpeech;

    private void Start()
    {
        playerPosition.anchoredPosition = positionArray[positionStart];
        positionNow = positionStart;
    }

    private void Update() 
    {
        if(isMoving) //pastiin dia gerak kalo kasih command
        {
            playerPosition.anchoredPosition = Vector2.MoveTowards(playerPosition.anchoredPosition, positionArray[positionNow], speed * Time.deltaTime);
            if(playerPosition.anchoredPosition == positionArray[positionNow])
            {
                Debug.Log(playerPosition.anchoredPosition);
                isChecking = true;
                isMoving = false;
                OrderChecker();
            }
        }
    }

    private void StartMoving(int positionTarget)
    {
        isMoving = true;
        positionNow = positionTarget;
        Debug.Log("move" + positionNow);
    }

    public void MoveUp()
    {
        if(isMoving || isChecking) return;
        if(positionNow - 3 >= 0)
        {
            currentMovement = Movement.UP;
            StartMoving(positionNow - 3);
        }
    }
    public void MoveDown()
    {
        if(isMoving || isChecking) return;
        if(positionNow + 3 < positionArray.Length)
        {
            currentMovement = Movement.DOWN;
            StartMoving(positionNow + 3);
        }
    }
    public void MoveRight()
    {
        if(isMoving || isChecking) return;
        if(positionNow + 1 < positionArray.Length && (positionNow + 1) / 3 == positionNow / 3)
        {
            currentMovement = Movement.RIGHT;
            StartMoving(positionNow + 1);
        }
    }
    public void MoveLeft()
    {
        if(isMoving || isChecking) return;
        if(positionNow - 1 >= 0 && (positionNow - 1) / 3 == positionNow / 3)
        {
            currentMovement = Movement.LEFT;
            StartMoving(positionNow - 1);
        }
    }

    public void OrderChecker()
    {
        if(currentMovement == correctOrder[currentOrder])
        {
            currentOrder++;
            if(currentOrder >= correctOrder.Length)
            {
                gridPuzzleSpeech.StopSpeech();
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
            else isChecking = false;
        }
        else
        {
            gridPuzzleSpeech.StopSpeech();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
