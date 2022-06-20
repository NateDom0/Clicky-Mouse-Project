using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    private GameManager gameManager; // get reference to GameManager script
    private Rigidbody targetRb;
    private float minSpeed = 12; // 12
    private float maxSpeed = 16; // 16
    private float maxTorque = 10; // 10
    private float xRange = 4; // 4
    private float ySpawnPos = -2; // -2
    private AudioSource getSoundEffect;
    
    public int pointValue; //each prefab has own value
    public ParticleSystem explosionParticle;
    public AudioClip bombSound;
    public AudioClip goodSound;


    // Start is called before the first frame update
    void Start()
    {
        getSoundEffect = GetComponent<AudioSource>();
        targetRb = GetComponent<Rigidbody>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>(); // get reference to Game Manager 
        targetRb.AddForce(RandomForce(), ForceMode.Impulse);
        targetRb.AddTorque(RandomTorque(), RandomTorque(), RandomTorque(), ForceMode.Impulse);

        transform.position = RandomSpawnPos();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    Vector3 RandomForce()
    {
        return Vector3.up * Random.Range(minSpeed, maxSpeed);
    }

    float RandomTorque()
    {
        return Random.Range(-maxTorque, maxTorque);
    }

    Vector3 RandomSpawnPos()
    {
        return new Vector3(Random.Range(-xRange, xRange), ySpawnPos);
    }


    // when 'bad' object triggers sensor(enabled), delete the object, otherwise, game over
    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
        
        // if game object is NOT a bad object, prompt game over
        //NEW: If good object triggers and game is still active
        if(!gameObject.CompareTag("Bad") && gameManager.isGameActive)
        {
           gameManager.UpdateLives(-1); // decrease lives by 1 for each object
        }
    }

    public void DestroyTarget()
    {
        if(gameManager.isGameActive && !gameManager.paused)
        {
            if(gameObject.CompareTag("Bad"))
            {
                AudioSource.PlayClipAtPoint(bombSound, transform.position);
                Destroy(gameObject);
                Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);
                gameManager.UpdateScore(pointValue);
            }
            else
            {
                AudioSource.PlayClipAtPoint(goodSound, transform.position);
                Destroy(gameObject);
                Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);
                gameManager.UpdateScore(pointValue);
            }
            
        }
    }

    /*
    // when user clicks down on mouse key
    private void OnMouseDown()
    {
        if(gameManager.isGameActive && !gameManager.paused)
        {
            //getSoundEffect.PlayOneShot(bombSound);
            Destroy(gameObject);
            Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);
            gameManager.UpdateScore(pointValue);
        }
        
    }
    */
}
