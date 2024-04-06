using System;
using Unity.VisualScripting;
using UnityEngine;

public class CharacterInteract : MonoBehaviour
{
    private GameObject cube;
    private bool isAttached = false;
    private GameObject hoverObject;
    void Update()
    {
        if (isAttached && Input.GetKeyUp(KeyCode.Space) && cube != null)
        {
            DetachCubeFromHand();
        }

        if (hoverObject != null && cube == null)
        {
            if (!isAttached && hoverObject.CompareTag("Cube") && Input.GetKeyUp(KeyCode.Space))
            {
                AttachCubeToHand(hoverObject.gameObject);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        hoverObject = other.gameObject;
    }

    private void OnTriggerExit(Collider other)
    {
        hoverObject = null;
    }

    void AttachCubeToHand(GameObject cubeToAttach)
    {
        cube = cubeToAttach;
        cube.transform.SetParent(transform);
        cube.transform.localPosition = Vector3.zero;
        isAttached = true;
        hoverObject = null;
    }

    void DetachCubeFromHand()
    {
        cube.transform.SetParent(null);
        isAttached = false;
        cube = null;
    }
}