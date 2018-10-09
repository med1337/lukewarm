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
    [SerializeField] int vision_radius = 5;
    [SerializeField] float shooting_delay = 1.0f;
    [SerializeField] float burst_shooting_delay = 0.4f;

    [Header("Accuracy - (Lower is better)")]
    [SerializeField] float pistol_accuracy = 0.1f;
    [SerializeField] float rifle_accuracy = 0.3f;
    [SerializeField] float shotgun_accuracy = 0.5f;

    [Header("Gun sprites")]
    [SerializeField] Sprite pistol_sprite;
    [SerializeField] Sprite rifle_sprite;
    [SerializeField] Sprite shotgun_sprite;
    [SerializeField] GameObject equipped_gun_sprite;

    private int shotgun_pellet_num = 5;
    private int burst_counter = 0;
    private float shooting_timer = 0.0f;
    private float burst_shooting_timer = 0.0f;

    private Transform player_ref;

    // Use this for initialization
    void Start()
    {
        player_ref = GameObject.FindGameObjectWithTag("MainCamera").transform;

        switch(equipped_gun)
        {
            case GUN.Pistol:
                equipped_gun_sprite.GetComponent<SpriteRenderer>().sprite = pistol_sprite;
                return;
            case GUN.Rifle:
                equipped_gun_sprite.GetComponent<SpriteRenderer>().sprite = rifle_sprite;
                return;
            case GUN.Shotgun:
                equipped_gun_sprite.GetComponent<SpriteRenderer>().sprite = shotgun_sprite;
                equipped_gun_sprite.transform.localScale *= 2;
                return;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!TimeManager.Instance.MovementDetected) return;            

        shooting_timer += TimeManager.Instance.TimeScale;
        burst_shooting_timer += TimeManager.Instance.TimeScale;

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

            RaycastHit hit;
            if(Physics.Raycast(transform.position, (player_ref.position - transform.position), out hit, vision_radius))
            {
                if (hit.collider.gameObject.GetComponent<CameraMovement>())
                {
                    float minimum_distance = 3.0f;

                    if (hit.distance > minimum_distance)
                    {
                        shooting_timer = 0.0f;
                        burst_shooting_timer = 0.0f;

                        if (equipped_gun == GUN.Shotgun)
                        {
                            for (int i = 0; i < shotgun_pellet_num; i++)
                            {
                                GameObject bullet = Instantiate(bullet_prefab, equipped_gun_sprite.transform.position, Quaternion.identity);
                                bullet.GetComponent<bullet_movement>().SetTarget(player_ref, shotgun_accuracy);
                            }
                        }

                        else
                        {
                            GameObject bullet = Instantiate(bullet_prefab, transform.position, Quaternion.identity);

                            if (equipped_gun == GUN.Rifle)
                            {
                                burst_counter++;
                                bullet.GetComponent<bullet_movement>().SetTarget(player_ref, rifle_accuracy);
                            }
                            else
                            {
                                bullet.GetComponent<bullet_movement>().SetTarget(player_ref, pistol_accuracy);
                            }

                        }
                    }
                }
            }
        }
    }
}