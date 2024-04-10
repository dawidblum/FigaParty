using System;
using System.Collections;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpinWheel : MonoBehaviour
{
    // Speed at which the wheel initially rotates
    public float startSpeed = 500f;
    // Speed reduction rate when slowing down
    public float slowDownRate = 50f;

    // Flag to check if the wheel is spinning
    private bool isSpinning = false;
    // Current rotation speed
    private float currentSpeed;

    public AudioSource source;
    public AudioClip clip;
    public bool changeSound;
    public bool changed;

    public bool changeLevel;
    public bool changing;
    
    private void Start() {
        Invoke(nameof(StartSpinning),2);
        startSpeed = Random.Range(300, 500);
    }

    // Update is called once per frame
    void Update()
    {
        if (changeLevel && !changing) {
            changing = true;
            if (GameManager.Instance.level > 3) {
                GameManager.Instance.level = 0;
                SceneLoader.Instance.LoadScene(GameManager.Instance.level);
            }
            else {
                SceneLoader.Instance.LoadScene(GameManager.Instance.level);
                GameManager.Instance.level++;    
            }
            
        }
        
        if (changeSound && !changed) {
            source.clip = clip;
            source.loop = false;
            changed = true;
        }
        // Check if the wheel is spinning
        if (isSpinning)
        {
            // Rotate the wheel to the left
            transform.Rotate(Vector3.back * currentSpeed * Time.deltaTime);

            // Reduce the speed gradually until it reaches zero
            currentSpeed -= slowDownRate * Time.deltaTime;

            if (currentSpeed < 100) {
                changeSound = true;
            }
            // If the speed is below a threshold, stop spinning
            if (currentSpeed <= 0f)
            {
                currentSpeed = 0f;
                isSpinning = false;
                changeLevel = true;
            }
        }
    }

    // Method to start the spinning
    public void StartSpinning()
    {
        source.Play();
        // Set the flag to true
        isSpinning = true;
        // Set the current speed to the starting speed
        currentSpeed = startSpeed;
    }
}