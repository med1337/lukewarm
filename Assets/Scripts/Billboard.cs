using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour {

    private Transform player_ref;

    // Use this for initialization
    void Start()
    {
        player_ref = GameObject.FindGameObjectWithTag("MainCamera").transform;
    }

    // Update is called once per frame
    void FixedUpdate ()
    {
        if (!TimeManager.Instance.MovementDetected) return;

        transform.LookAt(player_ref.position);
    }
}
