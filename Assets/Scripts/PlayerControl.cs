using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Commenting out Boundary for ship as it should now "warp" when it collides with boundary
[System.Serializable]
public class Boundary
{
    public float xMin, xMax, yMin, yMax, zMin, zMax;

}
*/

public class PlayerControl : MonoBehaviour
{

    public float speed;
    public float tilt;
//    public Boundary boundary;

    public GameObject shot;
    public Transform shotSpawn;
    public float fireRate;

    private float velocity;
    private float nextFire;
    private Rigidbody rb;
    private AudioSource audioSource;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();

        // Ship is stationary to start set velocity = 0
        velocity = 0;
    }

    private void Update()
    {

        if(Input.GetButton("Fire1") && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
            audioSource.Play();
        }

        if (Input.GetButton("Fire3"))
        {
            // TODO: Add more advanced acceleration
            velocity += 0.001f;
        }
    }

    private void FixedUpdate()
    {
        float rotateHorizontal = Input.GetAxis ("Horizontal");
        float rotateVertical = Input.GetAxis ("Vertical");


        rb.transform.Rotate(rotateVertical, rotateHorizontal, 0.0f);
        // Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);

        // rb.velocity = movement * speed;
        // need to modify velocity more to be based on speed of accceleration of ship
        rb.transform.position += transform.forward * velocity;

        // this clamps the ship between the boundaries
        // don't need to clamp it anymore as boundary will now "warp" to opposite side commenting out
        /* rb.position = new Vector3
        (
            Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax),
            Mathf.Clamp(rb.position.y, boundary.yMin, boundary.yMax),
            Mathf.Clamp(rb.position.z, boundary.zMin, boundary.zMax)
        );
        */

        // Commenting out for now as we do not want to tilt
        // Tilt to left or right
        // rb.rotation = Quaternion.Euler(0.0f, 0.0f, rb.velocity.x * -tilt);
    }
}