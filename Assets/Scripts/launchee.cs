﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class launchee : MonoBehaviour
{
    public Vector3 position;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    
        if ((transform.position - position).magnitude < 0.5)
        {
            this.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (GetComponent<Rigidbody>().velocity.magnitude == 0 && other.tag == "launcher")
        {
            GameObject otherObject = other.gameObject;
            launcher launcherScript = otherObject.GetComponent<launcher>();
            launcherScript.taunted();
            Destroy(gameObject);
        }
    }
}
