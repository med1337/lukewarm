using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Vector3 movement;

   [SerializeField] private float speed;


    // Use this for initialization
    void Start()
    {
        movement = new Vector3(0, 0,0);
    }




    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxis("Horizontal");

        movement.y = Input.GetAxis("Vertical");

        TimeManager.Instance.MovementDetected = movement != Vector3.zero;
        TimeManager.Instance.Scale = movement.magnitude;

        transform.position += movement * Time.deltaTime * speed;
    }
}