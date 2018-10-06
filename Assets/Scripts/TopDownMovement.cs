using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownMovement : MonoBehaviour {

    Animator anim;
    bool up, down, left, right = false;
    bool m_FacingRight = true;
    Vector3 pos;
    // Use this for initialization
    void Start ()
    {
        anim = this.GetComponent<Animator>();
        pos = transform.position;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            up = true;
            anim.SetInteger("State", 1);
        }
        else if (Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.W))
        {
            up = false;
            anim.SetInteger("State", 0);
        }

        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            down = true;
            anim.SetInteger("State", 1);
        }
        else if (Input.GetKeyUp(KeyCode.DownArrow) || Input.GetKeyUp(KeyCode.S))
        {
            down = false;
            anim.SetInteger("State", 0);
        }

        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            if (!m_FacingRight)
                Flip(); 
            right = true;
            anim.SetInteger("State", 2);
        }
        else if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.D))
        {
            right = false;
            anim.SetInteger("State", 0);
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            if (m_FacingRight)
                Flip(); 
            left = true;
            anim.SetInteger("State", 2);
        }
        else if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.A))
        {
            left = false;
            anim.SetInteger("State", 0);
        }

        if (up)
            pos.y += 0.1f;
        if (down)
            pos.y -= 0.1f;
        if (left)
            pos.x -= 0.1f;
        if (right)
            pos.x += 0.1f; 

        transform.position = pos;
    }

    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        m_FacingRight = !m_FacingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
