using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Player : MonoBehaviour
{
    public Color color;

    public string leftKey;
    public string rightKey;
    public string forwardKey;
    public string captureKey;
    public string fireKey;

    //Stores input from the PlayerInput
    private Vector3[] movementVectors = new Vector3[] {new Vector3(1f, 0f, 0f), new Vector3(0.5f,0.5f,0f),
                        new Vector3(-0.5f,0.5f,0f), new Vector3(-1f,0f,0f), new Vector3(-0.5f,-0.5f,0f), new Vector3(0.5f,-0.5f,0f)};
    private int d = 0;
    private Vector3 direction;

    bool hasMoved;
    void Update()
    {
        if (Input.GetKeyDown(leftKey))
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
        if (Input.GetKeyDown(rightKey))
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
        if (Input.GetKeyDown(forwardKey))
        {
            GetMovementDirection();
        }
    }

    void GetMovementDirection()
    {
        direction = movementVectors[d];
        transform.position += direction;
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Tower")
        {
            Debug.Log("Collision");
            if (Input.GetKey(captureKey))
            {
                collision.gameObject.GetComponent<Tower>().capture(this.gameObject);
            }
            if (Input.GetKeyUp(captureKey))
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
