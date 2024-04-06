using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FillCube : MonoBehaviour
{
    public Material cubeMaterial;
    public float fillSpeed = 1f;
    
    private float fillHeight = -.5f;
    private bool isFilling = false;

    void Start()
    {
        cubeMaterial.SetFloat("_Fill_Height", fillHeight);
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            isFilling = true;
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
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