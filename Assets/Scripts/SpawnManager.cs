using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject powerPrefab;

    private float spawnPosX;
    private float spawnPosZ;
    private float spawnRange = 9.0f;
    private int enemyCount = 0;
    private int num = 0;

    // Start is called before the first frame update
    void Start()
    {
        //Instantiate(powerPrefab, getSpawnPos(), powerPrefab.transform.rotation);
        SpawnEnemy(num);
    }

    // Update is called once per frame
    void Update()
    {
        enemyCount = FindObjectsOfType<Enemy>().Length;
        if(enemyCount == 0)
        {
            num++;
            SpawnEnemy(num);
        }
    }

    void SpawnEnemy(int noe)
    {
        for(int i = 0; i < noe; i++)
        {
            Instantiate(enemyPrefab, getSpawnPos(), enemyPrefab.transform.rotation);
        }
        Instantiate(powerPrefab, getSpawnPos(), powerPrefab.transform.rotation);
    }

    private Vector3 getSpawnPos()
    {
        spawnPosX = Random.Range(-spawnRange, spawnRange);
        spawnPosZ = Random.Range(-spawnRange, spawnRange);
        Vector3 spawnPos = new Vector3(spawnPosX, 0, spawnPosZ);
        return spawnPos;
    }
}
