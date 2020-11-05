using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogMovement : MonoBehaviour
{
	[SerializeField] private Frog_Mainbehavior maincode;
	[SerializeField] private Rigidbody2D FrogRigid2D;
	[SerializeField] private Animator animator;
	
    void Update()
    {
		float Vel_Y = FrogRigid2D.velocity.y;
		
        if(!maincode.m_Grounded)
		{
			animator.SetBool("IsJumping", true);
			
			if (Vel_Y > 0) animator.SetBool("IsFalling", false);
			else animator.SetBool("IsFalling", true);
		}
		
		else animator.SetBool("IsJumping", false);
    }
}
