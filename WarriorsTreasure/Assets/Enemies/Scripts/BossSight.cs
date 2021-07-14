using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSight : MonoBehaviour {

    [SerializeField]
    private Boss enemy; // make sure you set the enemy this script is attached to to this variable 
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            enemy.Target = other.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            enemy.Target = null;
        }
    }
}
