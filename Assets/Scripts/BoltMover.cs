using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoltMover : MonoBehaviour
{

    public float speed;
    public float maxDistance;
    private Rigidbody rb;
    private Vector3 spawnLocation;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        spawnLocation = transform.position;
        rb.velocity = transform.forward * speed;
    }

    /*
    private void Update()
    {
        Debug.Log("Bolt distance: " + Vector3.Distance(spawnLocation, rb.position));

        // Hmmmm, distance may not actually work because it loops back!
        if (Vector3.Distance(spawnLocation, rb.position) > maxDistance)
        {
            Destroy(gameObject);
        }
    }
    */
}