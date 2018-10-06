using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class TimeManager : MonoSingleton<TimeManager>
{
    public bool MovementDetected;
    public float TimeScale;
    public float Scale;
    public float Timer = 0.0f;
    [SerializeField] private Text Title;
    [SerializeField] private Text timerText;


    private void Update()
    {
        TimeScale = UnityEngine.Time.deltaTime * Scale;
        if (MovementDetected)
        {
            Timer += Time.deltaTime;
            var ts = TimeSpan.FromSeconds(Timer);
            //string str = ts.Minutes + ":" + ts.Seconds + ":" + ts.Milliseconds;
            timerText.text = ts.ToString();
            Title.color = Color.red;
        }
        else
        {
            Title.color = Color.black;
        }
    }
}