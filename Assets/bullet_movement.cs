using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet_movement : MonoBehaviour {

    
    [SerializeField] float max_speed = 3.0f;
    TrailRenderer tr;
    private bool fired = false;

    private float timer = 0;
	// Use this for initialization
	void Start ()
	{
	    tr = GetComponent<TrailRenderer>();
	}
	
	// Update is called once per frame
	void Update ()
	{
        if (!TimeManager.Instance.MovementDetected)
        {
            //tr.time = Mathf.Infinity;
            //timer = 0;
            return;
        }

	    //timer += Time.deltaTime;
     //   tr.time = 1+ timer;
        if (fired)
        {
            float step = max_speed * Time.deltaTime;

            transform.position += transform.right * (Time.deltaTime * max_speed);
        }
	}

    public void SetTarget(Transform _target)
    {
        transform.right = _target.position - transform.position;

        fired = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        fired = false;
        this.gameObject.GetComponent<TrailRenderer>().time = 0.3f;
        Destroy(this.gameObject, 0.5f);
    }
}
