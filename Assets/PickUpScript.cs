using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpScript : MonoBehaviour {

    public Camera playerCam;
    public float speed = 1;
    public float throwSpeed = 0.1f; 
    Vector3 pos;
    Vector3 offset = new Vector3(0.15f, -0.19f, 0.35f);
    bool pickup = false;
    bool thrown = false; 
	// Use this for initialization
	void Start ()
    {

        pos = transform.position;
        //grabPos = playerCam.transform.position + offset;
	}

    // Update is called once per frame
    //void OnTriggerEnter2D(Collider2D col)
    //{
    //    transform.parent = col.transform; 
    //}

    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!pickup)
                pickup = true;
            else
                thrown = true; 
        }
        if (thrown)
        {
            transform.parent = null;
            transform.localPosition += new Vector3(0, 0, throwSpeed); 
        }
        else if (pickup)
        {
            transform.parent = playerCam.transform; 
            float step = speed * Time.deltaTime;
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, offset, step);
        }
    }
}
