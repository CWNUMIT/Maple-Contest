using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogPower : MonoBehaviour
{
    public LayerMask _playerLayer;
    public float _powerRadius;

    private void FixedUpdate() 
    {        
        Collider2D[] colliders = Physics2D.OverlapCircleAll(this.transform.position, _powerRadius, _playerLayer);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                MapleManager.SlowDown();
            }
        }
    }

  	private void OnDrawGizmos() 
	{
        Color color = Color.red;
        color.a = 0.1f;
        Gizmos.color = color;
		Gizmos.DrawSphere(this.transform.position, _powerRadius);
	}
}
