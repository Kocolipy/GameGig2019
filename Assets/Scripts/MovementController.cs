using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MovementController : MonoBehaviour
{
    //Stores input from the PlayerInput
    private int d = 0;

    private Vector2 movementInput;

    private Vector3 direction;

    bool hasMoved;
    void Update()
    {
        if (Input.GetKeyDown("a"))
        {
            if (d == 5)
            {
                d = 0;
            }
            else
            {
                d++;
            }
        }
        if (Input.GetKeyDown("d"))
        {
            if (d == 0)
            {
                d = 5;
            }
            else
            {
                d--;
            }
        }
        if (Input.GetKeyDown("w"))
        {
            GetMovementDirection();
        }
    }

    void GetMovementDirection()
    {
        Vector3[] movementVectors = new Vector3[] {new Vector3(1f, 0f, 0f), new Vector3(0.5f,0.5f,0f),
                        new Vector3(-0.5f,0.5f,0f), new Vector3(-1f,0f,0f), new Vector3(-0.5f,-0.5f,0f), new Vector3(0.5f,-0.5f,0f)};
        direction = movementVectors[d];
        transform.position += direction;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
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
        else
        {
            transform.position -= direction;
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
