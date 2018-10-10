using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UltimateReplay;
using UltimateReplay.Core;
using UltimateReplay.Storage;
using UnityEngine;
using UnityEngine.UI;

public class TimeManager : MonoSingleton<TimeManager>
{
    public bool MovementDetected;
    public float TimeScale;
    public float Scale;
    public float Timer = 0.0f;
    public float RealtimeTimer = 0.0f;
    [SerializeField] private Text Title;
    [SerializeField] private Text timerText;
    [SerializeField] private Text reatltimeTimerText;

    
    public IEnumerator Load()
    {
        ResetTimers();
        yield return null;
        //Debug.Log(ReplayManager.IsDisposing);
        //Debug.Log(ReplayManager.Target.IsReplaying);
        //Debug.Log(ReplayManager.Target.IsReplaying);
        //Debug.Log(ReplayManager.Target.IsInvoking());
        //ReplayManager.BeginRecording(true);
    }


    public void ResetTimers()
    {
        RealtimeTimer = 0;
        Timer = 0;
    }

    private void Update()
    {
        TimeScale = UnityEngine.Time.deltaTime * Scale;
        RealtimeTimer += Time.deltaTime;
        var ts = TimeSpan.FromSeconds(RealtimeTimer);
        
        //string str = ts.Minutes + ":" + ts.Seconds + ":" + ts.Milliseconds;
        var deb = string.Format("{0:00}:{1:00}",
            ts.Minutes,
            ts.Seconds);
        reatltimeTimerText.text = deb;
        if (MovementDetected)
        {
            Timer += Time.deltaTime;
             ts = TimeSpan.FromSeconds(Timer);
            deb = string.Format("{0:00}:{1:00}",
                ts.Minutes,
                ts.Seconds);
            timerText.text = deb;
            Title.color = Color.red;
        }
        else
        {
            Title.color = Color.black;
        }
    }
}