using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JarsController : MonoBehaviour
{
    public float speed = 1f;
    public float stopDistance = 3f;

    private bool isMoving = false;
    private float targetPosition;

    [SerializeField] private List<Jar> jarFill;
    private int currentJar;
    private void Start()
    {
        jarFill[0].canFill = true;
    }

    void Update()
    {
        if (isMoving)
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);

            if (transform.position.x >= targetPosition)
            {
                isMoving = false;
                currentJar++;
                jarFill[currentJar].canFill = true;
            }
        }

        if (!isMoving && Input.GetKeyUp(KeyCode.Space))
        {
            targetPosition = transform.position.x + 3f;
            isMoving = true;
            jarFill[currentJar].canFill = false;
        }
    }
}