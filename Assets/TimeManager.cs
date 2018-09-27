using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class TimeManager : MonoSingleton<TimeManager>
{
    public bool MovementDetected;
    public float TimeScale;
    public float Scale;
    public float Timer = 0.0f;


    private void Update()
    {
        TimeScale = UnityEngine.Time.deltaTime * Scale;
        if (MovementDetected)
            Timer += Time.deltaTime;
    }
}