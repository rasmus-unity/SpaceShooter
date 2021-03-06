﻿using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class GameController : MonoBehaviour
{
    public GameObject[] hazards;
    public Vector3 spawnValues;
    public int hazardCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;
	
    public UnityEngine.UI.Text scoreText;
    public GameObject restartButton;
    public UnityEngine.UI.Text gameOverText;
	
    private bool gameOver;
    private bool restart;
    private int score;

    void Start ()
    {
        gameOver = false;
        restart = false;
        restartButton.SetActive (false);
        gameOverText.text = "";
        score = 0;
        UpdateScore ();
        StartCoroutine (SpawnWaves ());
    }

    void Update ()
    {
        if (restart)
        {
            if (CrossPlatformInputManager.GetButtonDown("Restart"))
            {
                SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);
            }
        }
    }

    IEnumerator SpawnWaves ()
    {
        yield return new WaitForSeconds (startWait);
        while (true)
        {
            for (int i = 0; i < hazardCount; i++)
            {
                GameObject hazard = hazards [Random.Range (0, hazards.Length)];
                Vector3 spawnPosition = new Vector3 (Random.Range (-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate (hazard, spawnPosition, spawnRotation);
                yield return new WaitForSeconds (spawnWait);
            }
            yield return new WaitForSeconds (waveWait);
			
            if (gameOver)
            {
                restartButton.SetActive (true);
                restart = true;
                break;
            }
        }
    }

    public void AddScore (int newScoreValue)
    {
        score += newScoreValue;
        UpdateScore ();
    }

    void UpdateScore ()
    {
        scoreText.text = "Score: " + score;
    }

    public void GameOver ()
    {
        gameOverText.text = "Game Over!";
        gameOver = true;
    }
}