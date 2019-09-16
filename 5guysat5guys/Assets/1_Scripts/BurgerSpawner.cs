using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurgerSpawner : MonoBehaviour
{
    [SerializeField] GameObject whatToSpawn = default;
    [SerializeField] Vector2 startingPoint = new Vector2(0, 0);
    [SerializeField] int amountOfEnemies = 5;
    [SerializeField] int spawnRange = 5;

    private GameObject enemyClone;

    private void Start()
    {
        transform.position = startingPoint;

        for (int e = 0; e < amountOfEnemies; e++)
        {
            transform.position = new Vector3(Random.value, Random.value, 0);
            enemyClone = Instantiate(whatToSpawn, transform.position, whatToSpawn.transform.rotation);
            transform.position = startingPoint;
        }
    }
}