using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;
using UnityEngine.UI;

public class launcher : MonoBehaviour
{
    public GameObject projectile;
    public GameObject spot;

    private Vector3 direction;
    private string fireKey;

    private float cooldown = 0f;  // Cooldown on bomb throwing

    private float travelDistance = 0f;
    private float travelStep = 0.2f;
    private Player parentScript;

    // Start is called before the first frame update
    void Start()
    {
        spot.GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.green);
        spot.GetComponent<Renderer>().enabled = false;
        projectile.GetComponent<Renderer>().enabled = false;

        parentScript = transform.parent.gameObject.GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        direction = parentScript.direction.normalized;
        //Color myColor = Color.Lerp(Color.green, Color.red, cooldown / 3f);
        //this.GetComponent<Renderer>().material.SetColor("_EmissionColor", myColor);
        if (cooldown > 0)
        {
            cooldown -= Time.deltaTime;

            if (cooldown <= 0)
            {
                this.GetComponent<SpriteRenderer>().enabled = true;
            }
        }
        else if (parentScript.stunDuration == 0)
        {
            if (Input.GetKey(parentScript.fireKey))
            {
                spot.GetComponent<Renderer>().enabled = true;
                spot.transform.position = transform.position + direction * travelDistance;

                spot.GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.Lerp(Color.green, Color.red, travelDistance / 15));

                if (travelDistance > 15)
                {
                    dropBomb(transform.position);
                }

                travelDistance += travelStep;
            }
            else if (Input.GetKeyUp(parentScript.fireKey))
            {
                dropBomb(spot.transform.position);
                spot.transform.position = transform.position;
            }
        }
        else
        {
            spot.GetComponent<Renderer>().enabled = false;
            spot.transform.position = transform.position;
            travelDistance = 0;
        }
    }

    void dropBomb(Vector3 position)
    {
        // Reset variables
        cooldown = 5f;
        travelDistance = 0f;
        this.GetComponent<SpriteRenderer>().enabled = false;

        // Reset spot
        spot.GetComponent<Renderer>().enabled = false;
        spot.transform.position = transform.position;

        // Launch bomb
        GameObject bomb = Instantiate(projectile, transform.position, Quaternion.identity) as GameObject;
        bomb.GetComponent<launchee>().move(position);
    }
}
