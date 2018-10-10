using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathListener : MonoBehaviour {

    [SerializeField] float max_speed = 5.0f;
    [SerializeField] ParticleSystem death_particles;

    private bool dead = false;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (!dead)
        {
            if (GetComponent<Rigidbody>().velocity.magnitude > max_speed)
            {
                dead = true;
                GetComponent<SpriteRenderer>().enabled = false;
                GetComponent<BoxCollider>().enabled = false;

                death_particles.Play();                
            }            
        }
        else
        {
            if (!death_particles.IsAlive())
            {
                Destroy(this.gameObject);
            }
        }
	}
}
