using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class TimeManager : MonoSingleton <TimeManager>
    {
        public bool MovementDetected;
        public float Time;

        public float Scale;


    private void Update()
    {
        Time = UnityEngine.Time.deltaTime * Scale;
    }
}
