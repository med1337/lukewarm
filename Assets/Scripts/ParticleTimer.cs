using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleTimer : MonoBehaviour {

    private bool playing = false;
    private ParticleSystem ps;
    ParticleSystem.MainModule main;

	// Use this for initialization
	void Start ()
    {
        ps = GetComponent<ParticleSystem>();
        main = ps.main;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (!TimeManager.Instance.MovementDetected)
        {
            if (ps.IsAlive())
            {
                ps.Pause();
            }
        }

        else
        {
            if (ps.isPaused)
            {
                ps.Play();
            }
            
            main.simulationSpeed = TimeManager.Instance.Scale;
        }        
    }
}
