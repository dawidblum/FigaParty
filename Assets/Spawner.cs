using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour {
    [SerializeField] private GameObject entityPrefab;
    [SerializeField] private GameObject figaPrefab;
    private List<GameObject> spawners = new List<GameObject>();
    
   private void Awake() {
      foreach (Transform g in transform.GetComponentsInChildren<Transform>()) {
        spawners.Add(g.gameObject);
      }
   }

   private void Start() {
       SpawnEntities();
       SpawnEntities();
       SpawnEntities();
       SpawnEntities();
       
       var spawnForFiga = spawners[Random.Range(0, spawners.Count - 1)];
       Instantiate(figaPrefab, spawnForFiga.transform.position, quaternion.identity);
   }

   private void SpawnEntities() {
       foreach (var spawner in spawners) {
           var chanceToSpawn = Random.Range(0, 2);

           if (chanceToSpawn == 1) {
               Instantiate(entityPrefab, spawner.transform.position, quaternion.identity);
           }
       }
   }
}
