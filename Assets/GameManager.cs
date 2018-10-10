using System;
using System.Collections;
using System.Collections.Generic;
using UltimateReplay;
using UltimateReplay.Storage;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoSingleton<GameManager>
{
    public int currentLevel;
    public bool Started = false;
    public bool Hold = false;
    public bool Throw = false;
    public bool Replaying = false;
    private float holdTimer = 0.0f;
    public MenuController mc;
    public List<DeathListener> enemies = new List<DeathListener>();
    private bool pressed = false;
    // Use this for initialization
    void Start()
    {
        Application.targetFrameRate = 15;
        SceneManager.sceneLoaded += onLoaded;
    }

    private void onLoaded(Scene arg0, LoadSceneMode arg1)
    {
        if (arg0.buildIndex != 0)
        {
            var x =GameObject.FindGameObjectsWithTag("Enemy");
            foreach (var o in x)
            {
                enemies.Add(o.GetComponent<DeathListener>());
            }
        }
    }


    public void CheckWinState()
    {
        if (enemies.Count==0)
        {
            return;
        }
        bool win = enemies[0].dead;
        for (var index = 1; index < enemies.Count; index++)
        {
            var deathListener = enemies[index];
            win = win && deathListener.dead;
        }

        if (win)
        {
            LevelComplete();
        }

        enemies = new List<DeathListener>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Started && !Replaying)
            CheckWinState();
        if (pressed)
        {
            holdTimer += Time.deltaTime;
            if(holdTimer>0.2f)
            Hold = true;
        }
        if (Input.GetKeyUp(KeyCode.Return) && !Started)
        {
            StartGame(1);
        }

        if (Input.GetKeyUp(KeyCode.R))
        {
            RestartLevel();
            //StartGame(currentLevel);
        }

        if (Input.GetKeyUp(KeyCode.P))
        {
            LevelComplete();
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
    

    public void RestartLevel()
    {
        SceneManager.LoadScene(currentLevel);
    }


    public void LevelComplete()
    {
        Replaying = true;
        ReplayManager.StopRecording();
        ReplayManager.BeginPlayback();
        Time.timeScale = 2;
        TimeManager.Instance.ResetTimers();
        mc.gameObject.SetActive(true);
        mc.img.gameObject.SetActive(false);
        mc.start.text = "PRESS BUTTON TO START\nLEVEL " + (currentLevel+1);
        StartCoroutine(WaitForReplayFinish());
        //todo: display gameover screen;
        //Started = false;
        //SceneManager.LoadScene(0);
    }


    public IEnumerator WaitForReplayFinish()
    {
        while (ReplayManager.IsReplaying)
        {
            Debug.Log("replaying");
            yield return null;
        }
        yield return new WaitForSeconds(2f);

        Replaying = false;
        DestroyImmediate(GameObject.Find("ReplayManager"));
        //ReplayManager.Target.Reset();
        if (currentLevel != 3)
        {
            StartGame(currentLevel+1);
            StartCoroutine(TimeManager.Instance.Load());
        }
        else
        {
            mc.gameObject.SetActive(true);
            mc.img.gameObject.SetActive(true);
            SceneManager.LoadScene(0);
        }
    }


    public void StartGame(int level)
    {
        Time.timeScale = 1;
        Debug.Log(level);
        currentLevel = level ;
        Started = true;
        SceneManager.LoadScene(level);
        mc.gameObject.SetActive(false);
    }
}