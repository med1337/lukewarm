using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet_movement : MonoBehaviour {

    [SerializeField] float max_speed = 3.0f;
    private bool fired = false;
    private bool dead = false;

    private float timer = 0;
    private float death_timer = 0;
    private float lifespan = 3.0f;

    private LineRenderer lr;
	// Use this for initialization
	void Start ()
	{
        lr = GetComponent<LineRenderer>();
        lr.SetPosition(0, transform.position);
        lr.SetPosition(1, transform.position);
    }
	
	// Update is called once per frame
	void Update ()
	{
        if (!TimeManager.Instance.MovementDetected) return;

        death_timer += TimeManager.Instance.TimeScale;

        if (death_timer >= lifespan)
        {
            KillBullet();
        }

        if (fired)
        {
            lr.SetPosition(0, transform.position);

            float step = max_speed * Time.deltaTime;

            transform.position += transform.right * (Time.deltaTime * max_speed);
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
                Destroy(this.gameObject);
            }
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

        KillBullet();
        fired = false;
    }

    private void KillBullet()
    {
        this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        this.gameObject.GetComponent<BoxCollider2D>().enabled = false;

        dead = true;
    }
}
