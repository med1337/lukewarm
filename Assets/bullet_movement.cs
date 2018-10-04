using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet_movement : MonoBehaviour {

    [SerializeField] float max_speed = 3.0f;
    TrailRenderer tr;
    private bool fired = false;

    private float timer = 0;
    private float death_timer = 0;
    private float lifespan = 3.0f;
	// Use this for initialization
	void Start ()
	{
	    //tr = GetComponent<TrailRenderer>();
	}
	
	// Update is called once per frame
	void Update ()
	{
        if (!TimeManager.Instance.MovementDetected) return;

        death_timer += TimeManager.Instance.TimeScale;

        if (death_timer >= lifespan)
        {
            Destroy(this.gameObject);
        }

        if (fired)
        {
            float step = max_speed * Time.deltaTime;

            transform.position += transform.right * (Time.deltaTime * max_speed);
        }
	}

    public void SetTarget(Transform _target, float _accuracy)
    {
        Vector3 direction = _target.position - transform.position;

        direction.x += Random.Range(-_accuracy, _accuracy);
        direction.y += Random.Range(-_accuracy, _accuracy);

        transform.right = direction;

        fired = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            return;
        }

        fired = false;
        Destroy(this.gameObject);
    }
}
