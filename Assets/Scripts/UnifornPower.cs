using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnifornPower : MonoBehaviour
{

    void Start()
    {
        this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
    }


    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            this.gameObject.GetComponent<SpriteRenderer>().enabled = true;
        }
    }

}
