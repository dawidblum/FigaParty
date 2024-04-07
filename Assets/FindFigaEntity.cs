using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindFigaEntity : MonoBehaviour {
    [SerializeField] private bool isFiga;
    
    public float speed = 5f;
    public Transform target;
    public float obstacleDetectionRange = 2f;

    private Vector3 direction;

    void Start()
    {
        direction = target.position - transform.position;
        direction.Normalize();
    }

    void Update()
    {
        RaycastHit hit;

        // Check for obstacles in front of the object
        if (Physics.Raycast(transform.position, transform.forward, out hit, obstacleDetectionRange))
        {
            // Avoid the obstacle by turning left
            transform.Rotate(0, -90, 0);
        }
        else
        {
            // Move the object towards the target
            transform.Translate(direction * speed * Time.deltaTime);
        }
    }
}
