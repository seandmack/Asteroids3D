using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundaryWarp : MonoBehaviour
{

    void OnTriggerExit(Collider other)
    {
        // Transform for asteroids and shots
        other.attachedRigidbody.position = other.attachedRigidbody.position * -1;

        // Ship used the transform.position so need to set this
        other.attachedRigidbody.transform.position = other.attachedRigidbody.transform.position * -1;
    }
}