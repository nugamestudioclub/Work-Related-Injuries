using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxSpawner : MonoBehaviour
{
    // number of seconds between box spawns
    public float spawnInterval;

    // the prefab this spawner will use
    public GameObject boxPrefab;

    private float timer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            timer = 0f;
            Instantiate(boxPrefab, transform.position, Quaternion.identity);
        }
    }
}
