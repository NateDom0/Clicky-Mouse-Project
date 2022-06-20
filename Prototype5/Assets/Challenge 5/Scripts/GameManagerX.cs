using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManagerX : MonoBehaviour
{
    private int score;
    private float spawnRate = 1.5f;
    private float countDown = 60.0f;
    private float countDownLimit = 0;
    private float spaceBetweenSquares = 2.5f; 
    private float minValueX = -3.75f; //  x value of the center of the left-most square
    private float minValueY = -3.75f; //  y value of the center of the bottom-most square

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI timerText;
    public GameObject titleScreen;
    public GameObject pauseScreen;
    public GameObject instructionsPanel;
    public Button restartButton; 
    public List<GameObject> targetPrefabs;
    public bool isGameActive;
    public bool isPaused;
    
    
    
    // Start the game, remove title screen, reset score, and adjust spawnRate based on difficulty button clicked
    public void StartGame(int difficulty)
    {
        isGameActive = true;
        score = 0;
        spawnRate /= difficulty;

        StartCoroutine(SpawnTarget());
        StartCoroutine(UpdateTimer()); 
        
        UpdateScore(0);
        instructionsPanel.SetActive(false);
        titleScreen.SetActive(false);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P) && isGameActive)
        {
            ChangePaused();
        }
    }

    // While game is active spawn a random target
    IEnumerator SpawnTarget()
    {
        while (isGameActive)
        {
            yield return new WaitForSeconds(spawnRate);
            int index = Random.Range(0, targetPrefabs.Count);

            if (isGameActive)
            {
                Instantiate(targetPrefabs[index], RandomSpawnPosition(), targetPrefabs[index].transform.rotation);
            }
            
        }
    }

    // Set countdown timer
    IEnumerator UpdateTimer()
    {
        while (isGameActive && countDown >= countDownLimit)
        {
            yield return new WaitForSeconds(0.5f);
            timerText.text = "Time: " + countDown;

            countDown -= 1;


            /*
            if(countDown != countDownLimit)
            {
                countDown -= 1;
                // Debug.Log("Time is: " + countDown); //testing purposes
            }
            else
            {
                GameOver();
            }*/
        }
        GameOver();
        
    }




    // Generate a random spawn position based on a random index from 0 to 3
    Vector3 RandomSpawnPosition()
    {
        float spawnPosX = minValueX + (RandomSquareIndex() * spaceBetweenSquares);
        float spawnPosY = minValueY + (RandomSquareIndex() * spaceBetweenSquares);

        Vector3 spawnPosition = new Vector3(spawnPosX, spawnPosY, 0);
        return spawnPosition;

    }

    // Generates random square index from 0 to 3, which determines which square the target will appear in
    int RandomSquareIndex()
    {
        return Random.Range(0, 4);
    }

    // Update score with value from target clicked
    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
    }

    // Stop game, bring up game over text and restart button
    public void GameOver()
    {
        gameOverText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
        isGameActive = false;
    }

    // Restart game by reloading the scene
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ChangePaused()
    {
        if(!isPaused)
        {
            isPaused = true;
            pauseScreen.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            isPaused = false;
            pauseScreen.SetActive(false);
            Time.timeScale = 1;
        }
    }

    public void displayInstructions()
    {
        instructionsPanel.SetActive(true);
        titleScreen.SetActive(false);
    }

    public void GoBack()
    {
        instructionsPanel.SetActive(false);
        titleScreen.SetActive(true);
    }

}
