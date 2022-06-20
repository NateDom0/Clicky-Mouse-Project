using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // added
using UnityEngine.SceneManagement; // added
using UnityEngine.UI; // added


public class GameManager : MonoBehaviour
{
    private float spawnRate = 1.0f; //1.0
    private int score;
    private int lives; // set to 3

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI livesText;
    public bool isGameActive;
    public bool paused;
    public Button restartButton;
    public GameObject titleScreen;
    public GameObject pauseScreen;
    public GameObject instructionScene;
    public GameObject goBackButton;
    // public GameObject pauseGoHomeButton;
    public List<GameObject> targets;
    //public GameObjects[] targets2;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P) && isGameActive)
        {
            ChangePaused();
        }
    }

    // moved everything from Start()
    public void StartGame(int difficulty)
    {
        isGameActive = true; // (order of code matters) must be set before coroutine
        score = 0;
        spawnRate /= difficulty; // reduce spawnRate delay on higher difficulty

        StartCoroutine(SpawnTarget());
        UpdateScore(0);
        UpdateLives(3);

        instructionScene.gameObject.SetActive(false);
        titleScreen.gameObject.SetActive(false); // when game starts, hide title screen
    }

    // Pauses game when 'P' key is pressed
    public void ChangePaused()
    {
        if(!paused)
        {
            paused = true;  
            pauseScreen.SetActive(true); // pause screen enabled and displayed
            Time.timeScale = 0; // physics calculations are paused
        }
        else
        {
            paused = false; // disable pause screen and set it to inactive
            pauseScreen.SetActive(false);
            Time.timeScale = 1;
        }
    }

    
    IEnumerator SpawnTarget()
    {
        while(isGameActive)
        {
            yield return new WaitForSeconds(spawnRate);
            int index = Random.Range(0, targets.Count); // gives us count of how many objects are in that list
            Instantiate(targets[index]);
        }
    }


    // Keep track of lives
    public void UpdateLives(int liveCount)
    {
        lives += liveCount;  // live = live + liveCount
        livesText.text = "Lives: " + lives;

        // If lives hits 0, game over
        if(lives <= 0) 
        {
            GameOver();
        }
    }


    public void UpdateScore(int scoreToAdd) // change to public
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
    }

    public void GameOver()
    {
        restartButton.gameObject.SetActive(true); // prompt restart button when game over
        gameOverText.gameObject.SetActive(true);
        isGameActive = false;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // restart scene
    }

    public void displayInstructions()
    {
        instructionScene.gameObject.SetActive(true);
        titleScreen.gameObject.SetActive(false);
    }

    public void goBack()
    {
        titleScreen.gameObject.SetActive(true);
        instructionScene.gameObject.SetActive(false);
    }
}
