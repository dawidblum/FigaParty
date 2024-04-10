using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class FindFigaEntity : MonoBehaviour {
    [SerializeField] private bool isFiga;
    public float speed = 5f;
    public int layerMask1;
    public int layerMask2;
    public float rotationSpeed = 5f;

    private Vector3 direction;
    private Quaternion targetRotation;

    [SerializeField] private List<GameObject> skins = new List<GameObject>();
    
    void Start()
    {
        if (!isFiga) {
            skins.ForEach(skin => skin.SetActive(false));
            skins[Random.Range(0, skins.Count - 1)].SetActive(true);
        }

        direction = Random.onUnitSphere;
        direction.y = 0;
        transform.LookAt(transform.position + direction);
        targetRotation = transform.rotation;
    }

    void Update()
    {
        if(GameManager.Instance.gameStopped) return;
        
        transform.Translate(direction * speed * Time.deltaTime, Space.World);

        // Combine layer masks using bitwise OR operation
        int combinedLayerMask = (1 << layerMask1) | (1 << layerMask2);

        RaycastHit hit;
        if (Physics.Raycast(transform.position, direction, out hit, 0.5f, combinedLayerMask))
        {
            direction = Vector3.Reflect(direction, hit.normal);
            targetRotation = Quaternion.LookRotation(direction);
        }

        // Smoothly rotate towards the target rotation
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        // Debug draw ray
        Debug.DrawRay(transform.position, direction * 0.5f, Color.red);

        transform.rotation = new Quaternion(0, transform.rotation.y, 0, transform.rotation.w);
        transform.position = new Vector3(transform.position.x, 1, transform.position.z);
        
    }

    private void OnMouseDown() {
        if(!isFiga)
            SoundsManager.Instance.PlayAudioShot(AudioLibrary.SoundType.Incorrect_Person);
        else {
            SoundsManager.Instance.PlayAudioShot(AudioLibrary.SoundType.Correct_Person);
            Invoke(nameof(LoadLevel), 2);
        }
        
    }

    private void LoadLevel() {
        SceneLoader.Instance.LoadScene(4);
    }
}
