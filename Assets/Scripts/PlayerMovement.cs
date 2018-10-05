using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float horizontal;
    private float vertical;
    private bool jump = false;
    private bool crouch = false;
    [SerializeField] private CharacterController2D controller2D;
    [SerializeField] private AnimationController animControl;

    [SerializeField] private float speed;
    private Rigidbody2D rigidbody2D;

    // Use this for initialization
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        controller2D = GetComponent<CharacterController2D>();
        animControl = GetComponent<AnimationController>(); 
        horizontal = Input.GetAxisRaw("Horizontal");
    }


    // Update is called once per frame
    void FixedUpdate()
    {
      
        TimeManager.Instance.MovementDetected = rigidbody2D.velocity != Vector2.zero;
        TimeManager.Instance.Scale = rigidbody2D.velocity.normalized.magnitude;
        controller2D.Move(horizontal * Time.fixedDeltaTime , crouch, jump);
        animControl.ChangeAnim(jump, crouch); 
        //jump = false;


    }

    private void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal") * speed;
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            jump = true;
        }
        if (Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.W))
        {
            jump = false;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            crouch = true;
        }
        else if (Input.GetKeyUp(KeyCode.DownArrow) || Input.GetKeyUp(KeyCode.S))
        {
            crouch = false;
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            //todo: logic
            Debug.Log("Dead");
        }
    }
}