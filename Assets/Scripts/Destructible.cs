using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class Destructible : MonoBehaviour
{
    public float destructionTime = 1f;
    public BombController controller;

    [Range(0f, 1f)]
    public float itemSpawnChance = 0.2f;
    public GameObject[] spawnableItems;
    

    private void Start()
    {
       

        
    }

    private void OnDestroy()
    {
        if (spawnableItems.Length > 0 && Random.value < itemSpawnChance && gameObject)
        {
            int randomIndex = Random.Range(0, spawnableItems.Length);
            
            Instantiate(spawnableItems[randomIndex], transform.position, Quaternion.identity);
            Destroy(gameObject, destructionTime);
        }
    }
}
