using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;
using UnityEngine.UI;

public class launcher_right : MonoBehaviour
{

    public GameObject projectile;
    public GameObject spot;
    public GameObject sprite;

    private Vector3 direction;
    private float cooldown = 0f;  // Cooldown on bomb throwing

    private float travelDistance = 0f;
    private float travelStep = 0.2f;
     
    // Start is called before the first frame update
    void Start()
    {

        //spot.GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.green);
        spot.GetComponent<Renderer>().enabled = false;
        projectile.GetComponent<Renderer>().enabled = false;

    }

    // Update is called once per frame
    void Update()
    {

        Player playerScript = transform.parent.gameObject.GetComponent<Player>();
        direction = playerScript.direction.normalized;
        Color myColor = Color.Lerp(Color.green, Color.red, cooldown / 3f);
        // this.GetComponent<Renderer>().material.SetColor("_EmissionColor", myColor);

        if (cooldown > 0)
        {
            cooldown -= Time.deltaTime;
            if (cooldown <= 0)
            {
                sprite.GetComponent<SpriteRenderer>().enabled = true;
            }
        }

        else if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            spot.GetComponent<Renderer>().enabled = true;
        }

        else if (Input.GetKey(KeyCode.LeftShift))
        {

            spot.transform.position = transform.position + direction * travelDistance;
            
            spot.GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.Lerp(Color.green, Color.red, travelDistance / 20));

            if (travelDistance > 20)
            {
                dropBomb(transform.position);
            }

            travelDistance += travelStep;
        }

        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            dropBomb(spot.transform.position);
        }

        else
        {
            spot.transform.position = transform.position;
        }
    }

    void dropBomb(Vector3 position)
    {
        cooldown = 3f;
        sprite.GetComponent<SpriteRenderer>().enabled = false;
        travelDistance = 0f;
        GameObject bomb = Instantiate(projectile, transform.position, Quaternion.identity) as GameObject;
        launchee bombScript = bomb.GetComponent<launchee>();
        bombScript.target = position;

        bomb.GetComponent<Renderer>().enabled = true;
        bomb.GetComponent<Rigidbody2D>().velocity = position - bomb.transform.position;

        spot.GetComponent<Renderer>().enabled = false;
        spot.transform.position = transform.position;
    }

}
