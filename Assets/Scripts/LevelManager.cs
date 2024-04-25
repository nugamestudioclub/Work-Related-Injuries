using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }

    [SerializeField]
    private float levelDuration;

    private int score;

    private UserInterfaceManager timeScoreHUD;


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

            var uigos = GameObject.FindGameObjectsWithTag("UIObject");
            foreach (var ui in uigos)
            {
                if (ui.TryGetComponent<UserInterfaceManager>(out var component))
                {
                    timeScoreHUD = component;
                    break;
                }
            }

            if (timeScoreHUD != null)
            {
                timeScoreHUD.UpdateScore(0);
                timeScoreHUD.UpdateTime(levelDuration);
            }
        }
    }

    private void Update()
    {
        if (timeScoreHUD == null) return;

        // regenerating textmeshpro every frame is bleh but whatever
        // i want millisecond time display :blush:

        levelDuration -= Time.deltaTime;
        timeScoreHUD.UpdateTime(levelDuration);

        if (levelDuration < 0f)
        {
            GameEnd();
        }
    }
    
    private void GameEnd()
    {
        Time.timeScale = 0f;
        timeScoreHUD.ShowGameEnd(score);
    }

    public void ModifyScore(int amnt)
    {
        score += amnt;
        timeScoreHUD.UpdateScore(score);
    }

    public static bool targetTileOpen(Vector3 targetPos)
    {
        Collider2D[] colliders = Physics2D.OverlapBoxAll(targetPos, new Vector2(0.8f * tileSize, 0.8f * tileSize), 0f);
        return !LevelManager.containsSolidCollider(colliders);
    }

    public static bool BoundsOpen(Bounds bounds)
    {
        Collider2D[] colliders = Physics2D.OverlapBoxAll(bounds.center, bounds.size, 0f);
        return !LevelManager.containsSolidCollider(colliders);
    }

    public static bool containsSolidCollider(Collider2D[] colliders)
    {
        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject.layer == LayerMask.NameToLayer("Wall"))
            {
                return true;
            }
            if (collider.gameObject.CompareTag("Box"))
            {
                return true;
            }
            if (collider.gameObject.CompareTag("Player"))
            {
                return true;
            }
        }
        return false;
    }

    public static bool targetTileHasMovable(Vector3 targetPos)
    {
        Collider2D[] colliders = Physics2D.OverlapBoxAll(targetPos, new Vector2(0.8f * tileSize, 0.8f * tileSize), 0f);

        bool boxFound = false;
        bool playerFound = false;

        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject.layer == LayerMask.NameToLayer("Wall"))
            {
                return false;
            }
            if (collider.gameObject.CompareTag("Box"))
            {
                boxFound = true;
            }
            if (collider.gameObject.CompareTag("Player"))
            {
                playerFound = true;
            }
        }

        if (boxFound || playerFound)
        {
            return true;
        }
        return false;
    }

    public static bool TargetSpaceOpen(Vector2 position, Vector2 size)
    {
        Collider2D[] colliders = Physics2D.OverlapBoxAll(position, size, 0f);
        return !LevelManager.containsSolidCollider(colliders);
    }

}
