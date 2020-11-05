using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 거리가 6 이하라면 플레이어쪽으로 계산된 움직임을 한다!

public class Frog_Mainbehavior : MonoBehaviour
{
	[SerializeField] private float jumpPower;
	[SerializeField] private float jumpDirAdd;
	
	[SerializeField] private LayerMask m_WhatIsGround;
	[SerializeField] private Transform m_GroundCheck;
	
	[SerializeField] private GameObject Player;
	
	const float k_GroundedRadius = .05f;
	
	private Transform Player_Transform;
	private Rigidbody2D Player_Rigid2D;
	
	private Transform Self_Transform;
	private Rigidbody2D Self_Rigid2D;
	
	public bool m_Grounded;
	private bool m_PlayerRight = false;
	
	private bool JumpCool = true;
	private bool BounceCool = true;
	
	private void Awake()
	{
		
		Player_Transform = Player.GetComponent<Transform>();
		Player_Rigid2D = Player.GetComponent<Rigidbody2D>();
		
		Self_Transform = GetComponent<Transform>();
		Self_Rigid2D = GetComponent<Rigidbody2D>();
		
	}
	
    private void FixedUpdate()
	{
        CheckGround();
		
		Calculate4Attack();
		
		if (m_Grounded) LookatPlayer();
    }
	
	public void Calculate4Attack()
	{
		float Cx = (Player_Transform.position.x - Self_Transform.position.x);
		float Cy = (Player_Transform.position.y - Self_Transform.position.y);
		
		float Distance = Math_2D_distance(Cx, Cy);
		
		if ((Distance <= 1.8f)&&(BounceCool))
		{
			BounceCool = false;
			Player_Rigid2D.velocity = new Vector2(8*(Cx/Mathf.Abs(Cx)), 7);
			Invoke("Timer_BounceCool",1f);
		}
		
		if (m_Grounded)
		{
			if (Distance <= 11)
			{
				// jump
				if (JumpCool)
				{
					CancelInvoke("Timer_JumpCool");
					JumpCool = false;
					Jump();
				}
			}
			else if (Distance >= 14)
			{
				// dig into ground and follow player
			}
		}
		
	}
	
	private void CheckGround()
	{
		bool wasGrounded = m_Grounded;
		m_Grounded = false;
		
		// The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
		// This can be done using layers instead but Sample Assets will not overwrite your project settings.
		Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
		for (int i = 0; i < colliders.Length; i++)
		{
			if (colliders[i].gameObject != gameObject)
			{
				m_Grounded = true;
				if (wasGrounded)
					Invoke("Timer_JumpCool",0.5f);
			}
		}
	}
	
	private void LookatPlayer()
	{
		Vector3 Player = Player_Transform.position;
		Vector3 Self = Self_Transform.position;
		
		if (Player.x > Self.x)
		{
			m_PlayerRight = true;
			Self_Transform.localScale = new Vector3(-1,1,1);
		}
		
		if (Player.x <= Self.x)
		{
			m_PlayerRight = false;
			Self_Transform.localScale = new Vector3(1,1,1);
		}
	}
	
	// jump to player
	public void Jump()
	{	
		Vector2 PlV = Player_Rigid2D.velocity;
		float Dir = Direction_Player();
		
		//Dir += jumpDirAdd*(2+(PlV.x/Mathf.Abs(PlV.x)));
		
		Vector2 AtkDir = GetForceDirection(Dir);
		Self_Rigid2D.velocity = (AtkDir*jumpPower);
		Self_Rigid2D.velocity += new Vector2(0, 7f);
	}
	
	private void Timer_JumpCool()
	{
		JumpCool = true;
	}
	
	private void Timer_BounceCool()
	{
		BounceCool = true;
	}
	
		// calculatings
	
	// Getting direction to player
	float Direction_Player()
	{
		Vector3 PlayerPos = Player_Transform.position;
		Vector3 SelfPos = Self_Transform.position;
		return ( Mathf.Atan2(PlayerPos.y-SelfPos.y, PlayerPos.x-SelfPos.x) * Mathf.Rad2Deg );
	}
	
	// Getting private original force direction from self to player's position
	Vector2 GetForceDirection(float angle){
		
		Vector2 Pos2D = Get_Force_byAngle(angle);
		
		float RangeKey = Math_2D_distance(Pos2D.x,Pos2D.y);
		
		Pos2D = Pos2D/RangeKey;
		
		return Pos2D;
	}
	
	Vector2 Get_Force_byAngle(float angle){
		
		return new Vector2( Mathf.Cos(angle*Mathf.Deg2Rad), Mathf.Sin(angle*Mathf.Deg2Rad) ); 
	}
	
	float Math_2D_distance(float x, float y){
		
		return Mathf.Sqrt(Mathf.Pow(x,2)+Mathf.Pow(y,2));
	}
	
}
