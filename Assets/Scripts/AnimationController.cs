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
	
	public void ChangeAnim(bool up, bool down)
    {
        //If not grounded
        if (up || down)
        {
            //Animate up down
            anim.SetInteger("State", 1);
        }
        //Else if Moving (Needs a moving check here)
        else if (TimeManager.Instance.MovementDetected)
        {
            //Animate Moving
            anim.SetInteger("State", 2);
        }
        //Else stand 
        else
            anim.SetInteger("State", 0);
    }
}
