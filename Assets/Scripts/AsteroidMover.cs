using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidMover : MonoBehaviour {

    public float maxSpeed;
    private float speed;
    private Rigidbody rb;


	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        speed = Random.Range(-maxSpeed, maxSpeed);
        rb.velocity = transform.forward * speed;
	}
	
}
