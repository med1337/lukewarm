using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoSingleton<GameManager>
{
    public bool Started = false;


    // Use this for initialization
    void Start()
    {
        Application.targetFrameRate = 15;
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown && !Started)
        {
            StartGame();
        }

        if (Input.GetKeyUp(KeyCode.R))
        {
            StartGame();
        }

        if (Input.GetKeyUp(KeyCode.P))
        {
            GameOver();
        }
    }
    

    public void RestartGame()
    {
        SceneManager.LoadScene(1);
    }


    public void GameOver()
    {
        //todo: display gameover screen;
        Started = false;
        SceneManager.LoadScene(0);
    }


    public void StartGame()
    {
        Started = true;
        SceneManager.LoadScene(1);
    }
}