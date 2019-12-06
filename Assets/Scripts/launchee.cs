﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class launchee : MonoBehaviour
{
    public Vector3 target;
    private bool landed = false;
    private float timeToDespawn = 5f; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (!landed && (transform.position - target).magnitude < 0.5)
        {
            this.GetComponent<Rigidbody>().velocity = Vector3.zero;
            landed = true;

        }
        
        if (landed && timeToDespawn <= 0)
        { 
            Destroy(gameObject);
        }

        if (landed && timeToDespawn > 0)
        {
            timeToDespawn -= Time.deltaTime;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (GetComponent<Rigidbody>().velocity.magnitude == 0 && other.tag == "launcher")
        {
            GameObject otherObjectParent = other.gameObject.transform.parent.gameObject;
            Player playerScript = otherObjectParent.GetComponent<Player>();
            playerScript.stunned();            
            Destroy(gameObject);
        }
    }
}
