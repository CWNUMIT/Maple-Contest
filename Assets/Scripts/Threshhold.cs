using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Threshhold : MonoBehaviour
{
    public Transform _reviveTransform;

    private void OnTriggerExit2D(Collider2D other) 
    {
        if(other.gameObject != null)
        {
            other.transform.position = _reviveTransform.position;
        }   
    }
}
