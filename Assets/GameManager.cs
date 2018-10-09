using System.Collections;
using System.Collections.Generic;
using UltimateReplay;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoSingleton<GameManager>
{
    public bool Started = false;
    public bool Hold = false;
    public bool Throw = false;

    private float holdTimer = 0.0f;

    private bool pressed = false;
    // Use this for initialization
    void Start()
    {
        Application.targetFrameRate = 15;
    }


    // Update is called once per frame
    void Update()
    {
        if (pressed)
        {
            holdTimer += Time.deltaTime;
            if(holdTimer>0.2f)
            Hold = true;
        }
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

        pressed = Input.GetKey(KeyCode.Return);
        Throw = Input.GetKeyUp(KeyCode.Return);
        if (Throw && Hold)
        {
            holdTimer = 0;
               Throw = false;
            pressed = false;
            Hold = false;
        }
        else if (Throw)
        {
            holdTimer = 0;
               pressed = false;
            Hold = false;
        }

       
        if (Throw)
        {
            Debug.Log("Throw");

        }

        if (Hold)
        {
            Debug.Log("Hold");
        }
        
    }
    

    public void RestartGame()
    {
        SceneManager.LoadScene(1);
    }


    public void GameOver()
    {
        UltimateReplay.ReplayManager.StopRecording();
        ReplayManager.BeginPlayback();
        Time.timeScale = 2;
        //todo: display gameover screen;
        //Started = false;
        //SceneManager.LoadScene(0);
    }


    public void StartGame()
    {
        Started = true;
        SceneManager.LoadScene(1);
    }
}