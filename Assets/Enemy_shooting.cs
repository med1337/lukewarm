using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_shooting : MonoBehaviour {

    [SerializeField] GameObject bullet_prefab;
    [SerializeField] float vision_radius = 5.0f;
    [SerializeField] float shooting_delay = 1.0f;

    private float shooting_timer = 0.0f;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!TimeManager.Instance.MovementDetected) return;

        shooting_timer += Time.deltaTime;

        Collider2D hit_collider = Physics2D.OverlapCircle(transform.position, vision_radius);

        if (hit_collider.gameObject.GetComponent<PlayerMovement>())
        {
            if (shooting_timer > shooting_delay)
            {
                shooting_timer = 0.0f;
                GameObject bullet = Instantiate(bullet_prefab, transform.position, Quaternion.identity);
                //bullet.GetComponent<bullet_movement>().SetTarget(hit_collider.gameObject.transform.position);

                bullet.GetComponent<bullet_movement>().SetTarget(hit_collider.gameObject.transform);
            }
            
        }
    }
}
