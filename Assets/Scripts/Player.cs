using System;
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

    public GameObject launcher;
    public Sprite[] sprites = new Sprite[6];

    //Stores input from the PlayerInput
    private Vector3[] movementVectors = new Vector3[] {new Vector3(1f, 0f, 0f), new Vector3(0.5f,0.5f,0f),
                        new Vector3(-0.5f,0.5f,0f), new Vector3(-1f,0f,0f), new Vector3(-0.5f,-0.5f,0f), new Vector3(0.5f,-0.5f,0f)};

    public int d = 0;
    public Vector3 direction = Vector3.zero;
    public float stunDuration = 0f;

    bool hasMoved;

    // Use this for initialization
    void Start()
    {
        launcher = Instantiate(launcher, this.transform.position - movementVectors[d] * 0.3f + new Vector3(0,0.1f,-0.1f), Quaternion.identity, this.transform);
    }
    void Update()
    {
        launcher.transform.position = transform.position - movementVectors[d] * 0.3f + new Vector3(0, 0.1f, -0.1f);
        if (stunDuration > 0)
        {
            Debug.Log("Stunned");
            stunDuration -= Math.Min(stunDuration, Time.deltaTime);
        }
        else
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

            direction = movementVectors[d];

            if (Input.GetKeyDown(forwardKey))
            {
                GetMovementDirection();
            }

        }

    }

    void GetMovementDirection()
    {
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

            if (Input.GetKeyUp(captureKey) || stunDuration > 0)
            {
                collision.gameObject.GetComponent<Tower>().stopCapture(this.gameObject);
            }
        }
        else if (collision.collider.tag == "Projectile")
        {
            if (collision.gameObject.GetComponent<launchee>().landed)
            {
                stunned();
                Destroy(collision.gameObject);
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
        if (collision.collider.tag == "Tower" || stunDuration > 0)
        {
            collision.gameObject.GetComponent<Tower>().stopCapture(this.gameObject);
        }
    }

    public void stunned()
    {
        stunDuration = 3f;
        Debug.Log("stunned");
    }
}
