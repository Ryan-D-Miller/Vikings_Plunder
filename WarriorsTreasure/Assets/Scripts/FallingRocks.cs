using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingRocks : MonoBehaviour {
    public GameObject[] fallingRocks;
    public Transform[] rocksPos;
    private bool ready = true;
    private float readyTimer;
    private float readylDuration = 10f;
    // Use this for initialization
    void Start () {
        readyTimer = 0;
	}
	
	// Update is called once per frame
	void Update ()
    {
		if(ready == false)
        {
            readyTimer += Time.deltaTime;
            if(readyTimer >= readylDuration)
            {
                readyTimer = 0;
                transform.Translate(0, -4, 0);
                ready = true;
            }
        }
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "ThrownWeapon")
        {
            Debug.Log("The Axe has hit");
            if (ready == true)
            {
                for (int i = 0; i < fallingRocks.Length; i++)
                {
                    Instantiate(fallingRocks[i], rocksPos[i].position, Quaternion.identity);
                }
                transform.Translate(0, 4, 0);
                ready = false;
            }
        }
    }

}
