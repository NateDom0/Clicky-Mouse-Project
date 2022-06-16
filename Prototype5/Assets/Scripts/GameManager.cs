using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // added
using UnityEngine.SceneManagement; // added
using UnityEngine.UI; // added


public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;
    public bool isGameActive;
    public Button restartButton;
    public GameObject titleScreen;
    public List<GameObject> targets;
    //public GameObjects[] targets2;
    
    private float spawnRate = 1.0f;
    private int score;

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // moved everything from Start()
    public void StartGame()
    {
        isGameActive = true; // (order of code matters) must be set before coroutine
        score = 0;

        StartCoroutine(SpawnTarget());
        UpdateScore(0);

        titleScreen.gameObject.SetActive(false); // when game starts, hide title screen
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
}
