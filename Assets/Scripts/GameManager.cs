using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Transform spawnPoint;
    public GameObject player;
    public GameObject enemy;
    public GameObject CurrentEnemey;

    //Counter
    public float counter;
    //Time Between Spawns
    public float delay;
    //Amount
    public int enemeySpawns;
    //FailSafe to ensure exacly as wanted
    public bool spawned;

    void Update()
    {
        SpawnEnemies(enemeySpawns);
    }

    void SpawnEnemies(int amount)
    {
        counter = counter + Time.deltaTime;
        if (counter > delay && spawned)
        {
            for (int i = 0; i < amount; i++)
            {
                CurrentEnemey = Instantiate(enemy, spawnPoint.transform.position, spawnPoint.transform.rotation);
            }
            spawned = false;
            counter = 0;
        }
        else
        {
            spawned = true;
        }
    }
}
