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

    public Sprite[] sprites = new Sprite[6];

    //Stores input from the PlayerInput
    private Vector3[] movementVectors = new Vector3[] {new Vector3(1f, 0f, 0f), new Vector3(0.5f,0.5f,0f),
                        new Vector3(-0.5f,0.5f,0f), new Vector3(-1f,0f,0f), new Vector3(-0.5f,-0.5f,0f), new Vector3(0.5f,-0.5f,0f)};

    public int d = 0;
    private Vector3 direction;

    bool hasMoved;

    void Update()
    {
        if (Input.GetKeyDown(leftKey))
        {
           d++;
        }
        if (Input.GetKeyDown(rightKey))
        {
            d--;
        }
        d = (d % 6 + 6) % 6;
        this.GetComponent<SpriteRenderer>().sprite = sprites[d];

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
            if (Input.GetKey(captureKey) && (collision.gameObject.GetComponent<Tower>().capturingPlayer == this.gameObject || collision.gameObject.GetComponent<Tower>().capturingPlayer == null))
            {
                collision.gameObject.GetComponent<Tower>().capture(this.gameObject);
            }
            if (Input.GetKeyUp(captureKey))
            {
                collision.gameObject.GetComponent<Tower>().stopCapture(this.gameObject);
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
            collision.gameObject.GetComponent<Tower>().stopCapture(this.gameObject);
        }
    }
}
