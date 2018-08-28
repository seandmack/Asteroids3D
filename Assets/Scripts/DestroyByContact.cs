using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContact : MonoBehaviour {

    private GameController gameController;

    public GameObject explosion;
    public GameObject playerExplosion;
    public int scoreValue;

    void Start()
    {
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");

        if (gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent<GameController>();
        }
        if (gameController == null)
        {
            Debug.Log("Cannot find 'GameController' script");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Boundary")
        {
            return;
        }

        // Check if one asteroid is hitting another. If that is the case do nothing
        // TODO: Asteroids should really bounce off eachother
        if (other.tag == "Asteroid" && this.tag == "Asteroid")
        {
            return;
        }

        // If one OR the other object is an asteroid then decrease count of asteroids
        if (other.tag == "Asteroid" || this.tag == "Asteroid")
        {
            gameController.activeAsteroidCount--;
        }

        Instantiate(explosion, transform.position, transform.rotation);

        if(other.tag == "Player")
        {
            // First detatch the camera so as not to destroy
            Camera.main.transform.parent = null;
            Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
            gameController.GameOver();
        }
        gameController.AddScore(scoreValue);
        Destroy(other.gameObject);
        Destroy(this.gameObject);
    }

}
