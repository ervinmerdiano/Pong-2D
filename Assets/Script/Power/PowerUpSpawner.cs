using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    public GameObject[] powerUpPrefabs;  // assign 3 prefab berbeda
    public float spawnInterval = 10f; 
    public Vector2 spawnMin;            
    public Vector2 spawnMax;            

    private void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    IEnumerator SpawnRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);
            SpawnPowerUp();
        }
    }

    void SpawnPowerUp()
    {
        int index = Random.Range(0, powerUpPrefabs.Length);
        GameObject prefab = powerUpPrefabs[index];

        float x = Random.Range(spawnMin.x, spawnMax.x);
        float y = Random.Range(spawnMin.y, spawnMax.y);
        Vector3 spawnPos = new Vector3(x, y, 0);

        Instantiate(prefab, spawnPos, Quaternion.identity);
    }
}
