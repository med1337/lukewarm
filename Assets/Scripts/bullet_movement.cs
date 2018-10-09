using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet_movement : MonoBehaviour {

    [SerializeField] float max_speed = 3.0f;
    private bool fired = false;
    private bool dead = false;

    private float timer = 0;
    private float death_timer = 0;
    private float lifespan = 20.0f;
    private float max_lifespan = 30.0f;
    private Rigidbody mRigidbody;
    private LineRenderer lr;
	// Use this for initialization
	void Start ()
	{
	    mRigidbody = GetComponent<Rigidbody>();
        lr = GetComponent<LineRenderer>();
        lr.SetPosition(0, transform.position);
        lr.SetPosition(1, transform.position);
    }
	
	// Update is called once per frame
	void FixedUpdate ()
	{
        if (!TimeManager.Instance.MovementDetected) return;

        death_timer += TimeManager.Instance.TimeScale;

        if (death_timer >= lifespan)
        {
            KillBullet();
        }

        if (death_timer >= max_lifespan)
        {
            Destroy(this.gameObject);
        }
        
        if (fired)
        {
            lr.SetPosition(0, transform.position);

            if (lr.GetPosition(1) != (lr.GetPosition(0)))
            {
                float step1 = 3 * Time.deltaTime;

                Vector3 distance = Vector3.MoveTowards(lr.GetPosition(1), lr.GetPosition(0), step1);

                lr.SetPosition(1, distance);
            }

            float step = max_speed * Time.deltaTime;
            var newpos = transform.position;
            newpos += transform.forward * (Time.deltaTime * max_speed);
            mRigidbody.MovePosition(newpos);
        }

        if (dead)
        {
            if (lr.GetPosition(1) != (lr.GetPosition(0)))
            {
                float step = 20 * Time.deltaTime;

                Vector3 distance = Vector3.MoveTowards(lr.GetPosition(1), lr.GetPosition(0), step);

                lr.SetPosition(1, distance);
            }
            else
            {
                // Destroy(this.gameObject);
            }
        }
    }

    public void SetTarget(Transform _target, float _accuracy)
    {
        Vector3 direction = _target.position - transform.position;

        direction.x += Random.Range(-_accuracy, _accuracy);
        direction.y += Random.Range(-_accuracy, _accuracy);
        direction.z += Random.Range(-_accuracy, _accuracy);

        transform.forward = direction;

        fired = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name);
        if (collision.gameObject.tag == "Enemy")
        {
            return;
        }
        else if (collision.gameObject.tag == "MainCamera")
        {
            this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }

        KillBullet();        
    }

    private void KillBullet()
    {
        mRigidbody.isKinematic = true;
        this.gameObject.GetComponent<BoxCollider>().enabled = false;

        dead = true;
        fired = false;
    }
}
