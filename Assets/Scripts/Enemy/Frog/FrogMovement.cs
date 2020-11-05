using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogMovement : MonoBehaviour
{
	[SerializeField] private Frog_Mainbehavior maincode;
	[SerializeField] private Animator animator;

	float horizontalMove = 0f;
	bool jump = false;
	bool crouch = false;
	
    // Update is called once per frame
    void Update()
    {
        
    }
}
