using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxController : MonoBehaviour
{
    [SerializeField]
    private float throwSpeed;

    private Vector3 targetPos;
    private Vector3 nextTargetTile;

    private float tileSize;

    // Start is called before the first frame update
    void Start()
    {
        tileSize = LevelManager.tileSize;
        targetPos = transform.position;
        nextTargetTile = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // bugs:
        // doesnt account for edge case of throwing into empty tile with wall in next tile
        // player can move into space they just threw box into
        // box doesn't recognize player as a collider which occupies a tile

        if (transform.position != nextTargetTile)
        {
            transform.position = Vector3.MoveTowards(transform.position, nextTargetTile, throwSpeed * Time.deltaTime);
            if (transform.position == nextTargetTile)
            {
                if (nextTargetTile != targetPos)
                {
                    Vector3 potentialNextTile = nextTargetTile + (targetPos - nextTargetTile).normalized * tileSize;
                    if (LevelManager.targetTileOpen(potentialNextTile))
                    {
                        nextTargetTile = potentialNextTile;
                    }
                    else
                    {
                        targetPos = nextTargetTile;
                    }
                }
            }
        }
    }

    // Gives this box a trajectory as a relative number of tiles
    public void giveTrajectory(Vector3 impulse)
    {
        targetPos = transform.position + impulse * tileSize;
        nextTargetTile = transform.position + impulse.normalized * tileSize;
        if (!LevelManager.targetTileOpen(nextTargetTile))
        {
            nextTargetTile = transform.position;
            targetPos = transform.position;
        }
    }
}
