using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private List<Transform> spawnPoint = new List<Transform>(3);
    
    [SerializeField]
    private GameObject player, enemy, currentEnemy;

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
                currentEnemy = Instantiate(enemy, spawnPoint[0].transform.position, spawnPoint[0].transform.rotation);
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
