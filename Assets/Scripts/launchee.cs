using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class launchee : MonoBehaviour
{
    public Vector3 target;
    public bool landed = false;
    private float speed = 0.3f;
    private float timeToDespawn = 2f; 

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (!landed)
        {
            if ((target - transform.position).magnitude > 0.5)
            {
                transform.position += Vector3.Normalize(target - transform.position) * speed;
            } 
            else
            {
                landed = true;
            }
        } 
        else
        {
            if (timeToDespawn <= 0)
            {
                Destroy(gameObject);
            }   
            else
            {
                timeToDespawn -= Time.deltaTime;
            }
        }
    }

    public void move(Vector3 loc)
    {
        this.GetComponent<Renderer>().enabled = true;
        target = loc;
    }
}
