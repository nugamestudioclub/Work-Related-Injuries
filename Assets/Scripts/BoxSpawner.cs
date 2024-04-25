using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxSpawner : MonoBehaviour
{
    [SerializeField]
    private List<SerializableItemPair<int, BoxOrientationManager.BoxOrientation>> orientationWeights;

    private int weightMax;

    // number of seconds between box spawns
    public float spawnInterval;

    // the prefab this spawner will use
    public GameObject boxPrefab;

    private float timer = 0f;

    private Bounds spawnBounds;

    private void Awake()
    {
        foreach (var item in orientationWeights)
        {
            weightMax = Mathf.Max(weightMax, item.Item1);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        spawnBounds = new Bounds(transform.position, boxPrefab.GetComponent<BoxCollider2D>().bounds.size);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            timer = 0f;

            LevelManager.DestroyObjectsIn(spawnBounds);

            var go = Instantiate(boxPrefab, transform.position, Quaternion.identity);

            if (go.TryGetComponent<BoxOrientationManager>(out var component))
            {
                int value = Random.Range(0, weightMax);

                foreach (var item in orientationWeights)
                {
                    if (value < item.Item1)
                    {
                        component.SetOrientation(item.Item2);
                        break;
                    }
                }
            }
        }
    }
}
