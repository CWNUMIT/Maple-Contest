using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiddenGround : MonoBehaviour
{
    private void OnCollisionExit2D(Collision2D other) 
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            MapleManager.isOn = true;
        }
    }
}
