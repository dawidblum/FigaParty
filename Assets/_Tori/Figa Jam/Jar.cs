using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jar : MonoBehaviour
{
    public Material cubeMaterial;
    public GameObject jarContent;
    public float fillSpeed = 1f;
    
    public float fillHeight = -0.1f;
    private bool isFilling = false;
    public bool canFill;

    public JarsController jarsController;
    public Morpher morpher;
    public bool overfill;


    void Start() {

        cubeMaterial = morpher._finalMaterial;
        cubeMaterial.SetFloat("_Fill_Height", fillHeight);
    }

    
    void Update()
    {
        if(GameManager.Instance.gameStopped) return;
        if (!canFill) return; 
    
        if (Input.GetKeyDown(KeyCode.Space) && !isFilling)
        {
          jarsController.Pour();
        }
        
        
        if (Input.GetKey(KeyCode.Space))
        {
            isFilling = true;
            if (!jarsController.source.isPlaying) {
                jarsController.StopRoutine();
                jarsController.source.volume = 1;
                jarsController.source.Play();
            }
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            Debug.Log("Test");
            isFilling = false;
        }

        if (isFilling)
        {
            fillHeight += fillSpeed * Time.deltaTime;
            fillHeight = Mathf.Clamp(fillHeight, -.5f, .5f);
            cubeMaterial.SetFloat("_Fill_Height", fillHeight);
        }
    }
}