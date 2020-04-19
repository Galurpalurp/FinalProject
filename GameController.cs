using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{

    float currentTime = 0f;
    float startingTime = 30f;

    [SerializeField] Text countdownText;



    public GameObject[] hazards;
    public Vector3 spawnValues;
    public int hazardCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;

    public AudioSource musicSource;
    public AudioClip winMusic;
    public AudioClip loseMusic;
    

    public Text ScoreText;
    private int score;
    public Text restartText;
    public Text gameOverText;
    public Text winText;

    private bool gameOver;
    private bool restart;

    void Start()
    {

        currentTime = startingTime;
        gameOver = false;
        restart = false;
        restartText.text = "";
        gameOverText.text = "";
        score = 0;
        UpdateScore();
        StartCoroutine(SpawnWaves());
    }

    void Update()
    {

        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }

        currentTime -= 1 * Time.deltaTime;
        countdownText.text = currentTime.ToString("0");

        if (currentTime < 10f) 
        {
            countdownText.color = Color.red; 
        }

        if (currentTime <= 0)
        {
            currentTime = 0;
            gameOver = true;
            restart = true;
            GameOver();

        }

        if (restart)
        {
            if (Input.GetKeyDown(KeyCode.T))
            {
                Application.LoadLevel (Application.loadedLevel);
            }
        }
    }
    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);
        while (true)
        {
            for (int i = 0; i < hazardCount; i++)
            {
                GameObject hazard = hazards[Random.Range (0,hazards.Length)];
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(hazard, spawnPosition, spawnRotation);
                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(waveWait);

            if (gameOver)
            {
                restartText.text = "Press 'T' for Restart";
                restart = true;
                break;
            }

           
        }
    }

    public void AddScore(int newScoreValue)
    {
        score += newScoreValue;
        UpdateScore();
    }

    void UpdateScore()
    {
        ScoreText.text = "Points: " + score;
        if (score >= 100)
        {
            winText.text = "You Win! Game Created by Nadyah Al-Ghamdi";
            gameOver = true;
            restart = true;
        }

        if (score >= 100)
        {
            musicSource.clip = winMusic;
            musicSource.Play();
        }
    }

    public void GameOver()
    {
        
        gameOverText.text = "Game Over!";
        gameOver = true;
        musicSource.clip = loseMusic;
        musicSource.Play();
    }
}
