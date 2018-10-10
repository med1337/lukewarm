using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnDisabled : MonoBehaviour {

    public void OnDisable()
    {
        if (this.enabled)
        {
            Destroy(this);
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
