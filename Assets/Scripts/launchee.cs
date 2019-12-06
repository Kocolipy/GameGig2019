using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class launchee : MonoBehaviour
{
    public Vector3 target;
    public float timeToDespawn = 5f; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    
        if ((transform.position - target).magnitude < 0.5)
        {
            this.GetComponent<Rigidbody>().velocity = Vector3.zero;
        } else if (timeToDespawn <= 0)
        {
            Destroy(gameObject);
        } else
        {
            timeToDespawn -= Time.deltaTime;
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
