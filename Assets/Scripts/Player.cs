using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;                //Floating point variable to store the player's movement speed.
    private Rigidbody2D rb2d;        //Store a reference to the Rigidbody2D component required to use 2D Physics.

    public Color color;

    // Use this for initialization
    void Start()
    {
        //Get and store a reference to the Rigidbody2D component so that we can access it.
        rb2d = GetComponent<Rigidbody2D>();
    }

    //FixedUpdate is called at a fixed interval and is independent of frame rate. Put physics code here.
    void FixedUpdate()
    {
        //Store the current horizontal input in the float moveHorizontal.
        float moveHorizontal = Input.GetAxis("Horizontal");

        //Use the two store floats to create a new Vector2 variable movement.
        Vector2 movement = new Vector2(moveHorizontal, 0);

        //Call the AddForce function of our Rigidbody2D rb2d supplying movement multiplied by speed to move our player.
        rb2d.AddForce(movement * speed);
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        //Capturing tower
        if (collision.collider.tag == "Tower")
        {
            if (Input.GetKey(KeyCode.DownArrow))
            {
                collision.gameObject.GetComponent<Tower>().capture(this.gameObject);
            }
            if (Input.GetKeyUp(KeyCode.DownArrow))
            {
                collision.gameObject.GetComponent<Tower>().stopCapture();
            }
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        //Exiting range of tower
        if (collision.collider.tag == "Tower")
        {
            collision.gameObject.GetComponent<Tower>().stopCapture();
        }
    }
}
