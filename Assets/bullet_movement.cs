using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet_movement : MonoBehaviour {

    
    [SerializeField] float max_speed = 3.0f;

    private bool fired = false;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update ()
    {
        if (!TimeManager.Instance.MovementDetected)
        {
            return;
        }
        
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
