using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This ensures that a TrailRenderer and BoxCollider are on the GameObject the script is attached to
[RequireComponent(typeof(TrailRenderer), typeof(BoxCollider))]

public class ClickAndSwipe : MonoBehaviour
{
    private GameManager gameManager;
    private Camera cam;
    private Vector3 mousePos;
    private TrailRenderer trail;
    private BoxCollider col;
    private bool swiping = false;

    
    // Start is called before the first frame update
    void Awake() // changed 'Start' to 'Awake'
    {
        cam = Camera.main;
        trail = GetComponent<TrailRenderer>();
        col = GetComponent<BoxCollider>();
        trail.enabled = false;
        col.enabled = false;

        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(gameManager.isGameActive ) //&& !gameManager.paused
        {
            if(Input.GetMouseButtonDown(0) && !gameManager.paused) // if left-mouse button is clicked, allow swipe
            {
                swiping = true;
                UpdateComponents();
            }
            else if(Input.GetMouseButtonUp(0)) // if left-mouse button released, no swipe
            {
                swiping = false;
                UpdateComponents();
            }

            if(swiping && !gameManager.paused)
            {
                UpdateMousePosition();
            }
        } 
    }

    // Sets up the GameObject to move with the mouse position
    void UpdateMousePosition()
    {
        // ScreenToWorld converts screen position of mouse to a world position
        mousePos = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10.0f));
        transform.position = mousePos;
    }

    // Used to update the TrailRenderer and BoxCollider, set the enabled state to whatever the swiping boolean is
    void UpdateComponents()
    {
        trail.enabled = swiping;
        col.enabled = swiping;
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.GetComponent<Target>())
        {
            // Destroy the target
            collision.gameObject.GetComponent<Target>().DestroyTarget();
        }        
    }
}

//Broken code
/*
void Update()
    {
        if(gameManager.isGameActive && !gameManager.paused)
        {
            swiping = true;
            UpdateComponents();          
        }
        else if(Input.GetMouseButtonUp(0) && !gameManager.paused)
        {
            swiping = false;
            UpdateComponents();
        }

        if(swiping && !gameManager.paused)
        {
            UpdateMousePosition();
        }
    }
    */