using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntranceAndExit : MonoBehaviour {
    private bool onExit = false;
    void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow) && onExit == true)
        {
            Debug.Log("trying to change scene");
            
        }
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")// when the player enters one of the entrance or exits currently the game pauses
        {
            Debug.Log("onExit is " + onExit);
            onExit = true;


        }
    }
    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")// when the player enters one of the entrance or exits currently the game pauses
        {
            Debug.Log("onExit is " + onExit);
            onExit = false;
        }
    }
}
