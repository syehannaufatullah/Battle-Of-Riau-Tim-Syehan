using System.Collections;
using UnityEngine;

public class ENMY_BoomSpawner : MonoBehaviour
{
    public GameObject objectToSpawn;         // GameObject yang ingin di-spawn
    public Transform[] spawnPoints;          // Array lokasi spawn

    private GameObject[] spawnedObjects;     // Objek yang sedang aktif

    void Start()
    {
        spawnedObjects = new GameObject[spawnPoints.Length];
        SpawnAll();
        StartCoroutine(CheckAndRespawn());
    }

    void SpawnAll()
    {
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            if (spawnedObjects[i] == null)
            {
                spawnedObjects[i] = Instantiate(objectToSpawn, spawnPoints[i].position, spawnPoints[i].rotation);
            }
        }
    }

    IEnumerator CheckAndRespawn()
    {
        while (true)
        {
            yield return new WaitForSeconds(10f);

            for (int i = 0; i < spawnedObjects.Length; i++)
            {
                if (spawnedObjects[i] == null)
                {
                    spawnedObjects[i] = Instantiate(objectToSpawn, spawnPoints[i].position, spawnPoints[i].rotation);
                }
            }
        }
    }
}
