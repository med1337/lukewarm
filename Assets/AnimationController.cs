using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    Animator anim;
    public GameObject charController; 

	void Start ()
    {
        anim = this.GetComponent<Animator>(); 
	}
	
	void Update ()
    {
        //If not grounded
        if (!gameObject.GetComponent<CharacterController2D>().m_Grounded)
        {
            //Animate Jump
            anim.SetInteger("State", 3);
        }
        //Else if Moving (Needs a moving check here)
        else
        {
            //Animate Moving
            anim.SetInteger("State", 2);
        }
        //Else stand 
        //else 
        //anim.SetInteger("State", 0);
    }
}
