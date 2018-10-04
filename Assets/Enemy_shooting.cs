using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_shooting : MonoBehaviour
{
    public enum GUN
    {
        Pistol,
        Rifle,
        Shotgun
    };

    [SerializeField] GUN equipped_gun = GUN.Pistol;
    [SerializeField] GameObject bullet_prefab;
    [SerializeField] float vision_radius = 5.0f;
    [SerializeField] float shooting_delay = 1.0f;
    [SerializeField] float burst_shooting_delay = 0.4f;

    [SerializeField] float pistol_accuracy = 0.1f;
    [SerializeField] float rifle_accuracy = 0.3f;
    [SerializeField] float shotgun_accuracy = 0.5f;

    private int shotgun_pellet_num = 5;
    private int burst_counter = 0;
    private float shooting_timer = 0.0f;
    private float burst_shooting_timer = 0.0f;


    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

        if (!TimeManager.Instance.MovementDetected) return;

        shooting_timer += TimeManager.Instance.TimeScale;
        burst_shooting_timer += TimeManager.Instance.TimeScale;

        //shooting_timer += Time.deltaTime;
        //burst_shooting_timer += Time.deltaTime;

        if ((equipped_gun == GUN.Rifle) && (burst_shooting_timer <= burst_shooting_delay))
        {
            return;
        }        

        if (((equipped_gun == GUN.Rifle) && (burst_counter < 3)) || (shooting_timer >= shooting_delay))
        {
            if (burst_counter >= 3)
            {
                burst_counter = 0;
            }

            Collider2D[] hit_collider =
                Physics2D.OverlapCircleAll(transform.position, vision_radius);
            foreach (var d in hit_collider)
            {
                if (d.gameObject.GetComponent<PlayerMovement>())
                {
                    shooting_timer = 0.0f;
                    burst_shooting_timer = 0.0f;

                    if (equipped_gun == GUN.Shotgun)
                    {
                        for (int i = 0; i < shotgun_pellet_num; i++)
                        {
                            GameObject bullet = Instantiate(bullet_prefab, transform.position, Quaternion.identity);
                            bullet.GetComponent<bullet_movement>().SetTarget(d.gameObject.transform, shotgun_accuracy);
                        }
                    }

                    else
                    {
                        GameObject bullet = Instantiate(bullet_prefab, transform.position, Quaternion.identity);

                        if (equipped_gun == GUN.Rifle)
                        {
                            burst_counter++;
                            bullet.GetComponent<bullet_movement>().SetTarget(d.gameObject.transform, rifle_accuracy);
                        }
                        else
                        {
                            bullet.GetComponent<bullet_movement>().SetTarget(d.gameObject.transform, pistol_accuracy);
                        }
                        
                    }
                    break;
                }
            }
        }
    }
}