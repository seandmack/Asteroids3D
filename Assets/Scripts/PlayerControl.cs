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
    public float acceleration;
    public float thrustSpeed;
    public float turnSpeed;
//    public float tilt;

    public GameObject shot;
    public Transform shotSpawn;
    public ThrustButtonResponder thrustButton;
    public FireButtonResponder fireButton;
    public float fireRate;

    private float nextFire;
    private Rigidbody rb;
    private AudioSource audioSource;
    private Quaternion calibrationQuaternion;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();

        // TODO: Add in options to allow player to calibrate on demand
        CalibrateAccelerometer();

        // Ship is stationary to start set thrustSpeed = 0
        thrustSpeed = 0;
    }

    private void Update()
    {

        // Updateing for touch buttons instead of keyboard, commenting out following line
        // if(Input.GetButton("Fire1") && Time.time > nextFire)
        if ( fireButton.CanFire() && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
            audioSource.Play();
        }

        // Updateing for touch buttons instead of keyboard, commenting out following line
        //if (Input.GetButton("Fire3"))
        if ( thrustButton.CanThrust() )
        {
            // TODO: Add more advanced acceleration
            thrustSpeed += 0.001f;
        }
    }

    private void FixedUpdate()
    {
        // switching to accelerometer input, commenting following lines out
        // float rotateHorizontal = Input.GetAxis ("Horizontal");
        // float rotateVertical = Input.GetAxis ("Vertical");

        Vector3 accelerationRaw = Input.acceleration;
        Vector3 acceleration = FixAcceleration(accelerationRaw) * turnSpeed;

        // Comment out following and replace for accelerometer input
        // rb.transform.Rotate(rotateVertical, rotateHorizontal, 0.0f);
        rb.transform.Rotate(acceleration.y, acceleration.x, 0.0f);

        // need to modify thrustSpeed more to be based on speed of accceleration of ship
        rb.transform.position += transform.forward * thrustSpeed;

        // Commenting out for now as we do not want to tilt
        // Tilt to left or right
        // rb.rotation = Quaternion.Euler(0.0f, 0.0f, rb.velocity.x * -tilt);
    }

    void CalibrateAccelerometer()
    //Used to calibrate the Iput.acceleration input
    {
        Vector3 accelerationSnapshot = Input.acceleration;
        Quaternion rotateQuaternion = Quaternion.FromToRotation(new Vector3(0.0f, 0.0f, -1.0f), accelerationSnapshot);
        calibrationQuaternion = Quaternion.Inverse(rotateQuaternion);
    }

    Vector3 FixAcceleration(Vector3 acceleration)
    //Get the 'calibrated' value from the Input
    {
        Vector3 fixedAcceleration = calibrationQuaternion * acceleration;
        return fixedAcceleration;
    }
}