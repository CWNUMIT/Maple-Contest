using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 거리가 6 이하라면 플레이어쪽으로 계산된 움직임을 한다!

public class Frog_Mainbehavior : MonoBehaviour
{
	[SerializeField] private float jumpPower = 500f;
	
	[SerializeField] private LayerMask m_WhatIsGround;
	[SerializeField] private Transform m_GroundCheck;
	
	[SerializeField] private GameObject Player_transform;
	
	private Transform Player_Transform;
	private Rigidbody2D Player_Rigid2D;
	
	public bool m_Grounded;
	private bool m_FacingRight = false;
	
	private Transform Self_Transform;
	private Rigidbody2D Self_Rigid2D;
	
	private Vector3 m_Velocity = Vector3.zero;
	
	private void Awake()
	{
		
		Player_Transform = GameObject.Find("Player").GetComponent<Transform>();
		Player_Rigid2D = GameObject.Find("Player").GetComponent<Rigidbody2D>();
		
		Self_Transform = GetComponent<Transform>();
		Self_Rigid2D = GetComponent<Rigidbody2D>();
		
	}
	
    // Update is called once per frame
    private void FixedUpdate()
	{
        
		LookatPlayer();
		
		Set_Face();
    }
	
	private void Set_Face()
	{
		if (Self_Transform.localScale.x == 1)
			m_FacingRight = false;
		else
			m_FacingRight = true;
	}
	
	private void LookatPlayer()
	{
		Vector3 Player = Player_Transform.position;
		Vector3 Self = Self_Transform.position;
		
		if ((Player.x > Self.x) && !(m_FacingRight))
			Flip();
		
		if ((Player.x <= Self.x) && (m_FacingRight))
			Flip();
	}
	
	public void Jump(Vector2 V2_dir)
	{	
		
		
		
	}
	
	//self events (?)
	
	private void Flip()
	{
		// Multiply the player's x local scale by -1.
		Vector3 theScale = Self_Transform.localScale;
		theScale.x *= -1;
		Self_Transform.localScale = theScale;
	}
	
	// calculatings
	Vector2 Direction_Player()
	{
		
		
		return new Vector2(0,0);
	}
	
}
