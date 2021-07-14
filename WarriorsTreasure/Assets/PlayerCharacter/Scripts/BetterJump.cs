using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetterJump : MonoBehaviour
{

    public float fallMultipler = 2.5f; // a float varible to add gravity as you fall to make you fall quicker makes it feel more gamy
    public float lowJumpMultipler = 2f;

    Rigidbody2D rb;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Player.Instance.axeHasHit == false)
        {
            if (rb.velocity.y < 0)
            {
                rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultipler - 1) * Time.deltaTime; // if the player is falling makes him fall just a little fast looks more correct for a video game
            }
            else if (rb.velocity.y > 0 && !Input.GetKey(KeyCode.Space))
            {
                rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultipler - 1) * Time.deltaTime;// if the player is going up but the jump button is not being pressed a little more gravity is applied for that duration
            }
        }
    }
}
