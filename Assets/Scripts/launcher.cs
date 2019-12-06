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

    private float cooldown = 0f;  // Cooldown on bomb throwing

    private float travelDistance = 0f;
    private float travelStep = 0.2f;

    private float taunt = 0f; // Duration left if stunned
     
    // Start is called before the first frame update
    void Start()
    {

        transform.forward = Vector3.right; // Debug
        spot.GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.green);
        spot.GetComponent<Renderer>().enabled = false;
        projectile.GetComponent<Renderer>().enabled = false;

    }

    // Update is called once per frame
    void Update()
    {

        if (taunt > 0)
        {
            taunt -= Math.Min(taunt, Time.deltaTime);

        }

        Color myColor = Color.Lerp(Color.green, Color.red, cooldown / 3f);
        this.GetComponent<Renderer>().material.SetColor("_EmissionColor", myColor);
        if (cooldown > 0)
        {
            cooldown -= Math.Min(cooldown, Time.deltaTime);
        }

        else if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            spot.GetComponent<Renderer>().enabled = true;
        }

        else if (Input.GetKey(KeyCode.LeftShift))
        {

            spot.transform.position = transform.position + transform.forward * travelDistance;
            
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
        travelDistance = 0f;
        GameObject bomb = Instantiate(projectile, transform.position, Quaternion.identity, transform) as GameObject;
        launchee bombScript = bomb.GetComponent<launchee>();
        bombScript.position = position;

        bomb.GetComponent<Renderer>().enabled = true;
        bomb.GetComponent<Rigidbody>().velocity = position - bomb.transform.position;

        spot.GetComponent<Renderer>().enabled = false;
        spot.transform.position = transform.position;
    }

    public void taunted()
    {
        taunt = 3.0f;
    }
}
