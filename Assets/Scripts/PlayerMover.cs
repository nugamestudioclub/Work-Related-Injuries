using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    [Header("World Properties")]

    // a Vector2 representing the amount of displacement from (0, 0) in world space to the center of the central tile's position in the level
    [SerializeField] private Vector3 gridOffset;

    // the amount of distance between centers of adjacent tiles in world space
    [SerializeField] private float tileSize;

    [Header("LayerMasks")]
    [SerializeField] private LayerMask wallLayer;

    [Header("Player Properties")]
    [SerializeField] private float walkSpeed;
    [SerializeField] private float sprintSpeed;
    

    // represents the world space position of the center of the tile this player occupies
    private Vector3 targetTile;

    // represents the current move speed of the player
    private float moveSpeed;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = gridOffset;
        targetTile = gridOffset;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift)) {
            moveSpeed = sprintSpeed;
        } else {
            moveSpeed = walkSpeed;
        }

        float xInput = Input.GetAxis("Horizontal");
        float yInput = Input.GetAxis("Vertical");

        transform.position = Vector3.MoveTowards(transform.position, targetTile, moveSpeed * Time.deltaTime);

        if (transform.position == targetTile) {
            // select next tile mechanic
            if (Mathf.Abs(yInput) == 1f) {
                attemptMoveTargetBy(new Vector3(0f, tileSize * yInput, 0f));
            } else if (Mathf.Abs(xInput) == 1f) {
                attemptMoveTargetBy(new Vector3(tileSize * xInput, 0f, 0f));
            }
        } else {
            // backup mechanic
            Vector3 displacement = targetTile - transform.position;
            if (Mathf.Abs(yInput) == 1f) {
                if (transform.position.x == targetTile.x) {
                    if (displacement.y * yInput < 0) {
                        attemptMoveTargetBy(new Vector3(0f, tileSize * yInput, 0f));
                    }
                }
            } else if (Mathf.Abs(xInput) == 1f) {
                if (transform.position.y == targetTile.y) {
                    if (displacement.x * xInput < 0) {
                        attemptMoveTargetBy(new Vector3(tileSize * xInput, 0f, 0f));
                    }
                }
            }
        }
    }

    private void attemptMoveTargetBy(Vector3 relativeMove) {
        if (targetTileOpen(targetTile + relativeMove)) {
            targetTile += relativeMove;
        } else {
            Debug.Log("Move failed");
        }
    }

    private bool targetTileOpen(Vector3 targetPos) {
        return !Physics2D.OverlapBox(targetPos, new Vector2(0.8f, 0.8f), 0f, wallLayer);
    }
}
