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
    }


    public void StartGame()
    {
        Started = true;
        SceneManager.LoadScene(1);
    }
}