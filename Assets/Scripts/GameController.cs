using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    private int score;
    private bool gameOver;
    private bool restart;
    public int activeAsteroidCount;

    public GameObject hazard;
    public Vector3 spawnValues;
    // public int hazardCount;
    public int startAsteroidCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;
    public Text scoreText;
    public Text gameOverText;
    public bool hazardsOn;      // Toggle hazards on and off to adjust game controls
    public GameObject restartButton;

    void Start()
    {
        score = 0;
        gameOver = false;
        restart = false;
        restartButton.SetActive(false);
        gameOverText.text = "";

        UpdateScore();

        if (hazardsOn)
        {
            // Spawn the waves of asteroids
            StartCoroutine(SpawnWaves());
        }
    }


    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);

        while(true)
        {
            // Psudocode for wave implementation
            // while !gamover
            //    spawnAsteroids(i)
            //    WaitWhile(!leveComplete);
            //    asteroids++

            activeAsteroidCount = startAsteroidCount;

            while(!gameOver)
            {
                SpawnAsteroids(activeAsteroidCount);
                yield return new WaitWhile(() => activeAsteroidCount > 0);
                activeAsteroidCount++;
                // yield return new WaitForSeconds(waveWait);
            }


            if (gameOver)
            {
                restartButton.SetActive(true);
                restart = true;
                break;
            }
        }
    }

    private void SpawnAsteroids(int asteroidCount)
    {
        // Spawn a single wave with hazrdCount number of asteroids
        for (int i = 0; i < asteroidCount; i++)
        {
            // Spawn at a random position on the sphere. Don't really care if it is facing the wrong
            // way as it will automatically jump to other side
            // TODO: Ideally the position should be dynamically determined by the radius of the boundary
            Vector3 spawnPosition = Random.onUnitSphere * 15;
            Quaternion spawnRotation = Quaternion.identity;
            GameObject newObject = Instantiate(hazard, spawnPosition, spawnRotation);

            newObject.transform.localScale = new Vector3(4, 4, 4); // change its local scale in x y z format

            // Wait between spawning new asteroids 
            // removed this based on initial game with all asteroids appearing at same time
            // yield return new WaitForSeconds(spawnWait);
        }
    }

    public void AddScore(int newScoreValue)
    {
        score += newScoreValue;
        UpdateScore();
    }

    void UpdateScore()
    {
        scoreText.text = "Score: " + score;
    }

    public void GameOver()
    {
        gameOverText.text = "Game Over";
        gameOver = true;
    }

    public void RestartGame()
    {
        Application.LoadLevel(Application.loadedLevel);
    }
}
