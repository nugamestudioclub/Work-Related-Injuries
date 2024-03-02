using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }

    [SerializeField]
    private float tileSizeInput;
    [SerializeField]
    private Vector3 gridOffsetInput;

    // the amount of distance in units between centers of adjacent tiles in world space
    public static float tileSize;
    // a Vector3 representing the amount of displacement from (0, 0, 0) in world space to the center of the central tile's position in the level
    public static Vector3 gridOffset;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Debug.Log("Duplicate LevelManager found in scene.");
            Destroy(this);
        } else
        {
            Instance = this;
            tileSize = tileSizeInput;
            gridOffset = gridOffsetInput;
        }
    }

    public static bool targetTileOpen(Vector3 targetPos)
    {
        Collider2D[] colliders = Physics2D.OverlapBoxAll(targetPos, new Vector2(0.8f * tileSize, 0.8f * tileSize), 0f);
        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject.layer == LayerMask.NameToLayer("Wall"))
            {
                return false;
            }
            if (collider.gameObject.name == "Box")
            {
                return false;
            }
            if (collider.gameObject.CompareTag("Player"))
            {
                return false;
            }
        }
        return true;
    }
}
