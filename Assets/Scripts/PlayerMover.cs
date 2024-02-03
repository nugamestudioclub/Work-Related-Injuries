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

    [Header("Player Properties")]
    [SerializeField] private float moveSpeed;
    

    // represents the world space position of the center of the tile this player occupies
    private Vector3 occupiedTile;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = gridOffset;
        occupiedTile = gridOffset;
    }

    // Update is called once per frame
    void Update()
    {
        float xInput = Input.GetAxis("Horizontal");
        float yInput = Input.GetAxis("Vertical");

        if (xInput == 0 && yInput == 0) {
            if (transform.position != occupiedTile) {
                transform.position = Vector3.MoveTowards(transform.position, occupiedTile, moveSpeed * Time.deltaTime);
            }
        } else {
            Vector3 destination = occupiedTile;
            if (Mathf.Abs(xInput) >= Mathf.Abs(yInput)) {
                if (transform.position.y == occupiedTile.y) {
                    destination += new Vector3(tileSize * (Mathf.Abs(xInput) / xInput), 0f, 0f);
                }
            } else {
                if (transform.position.x == occupiedTile.x) {
                    destination += new Vector3(0f, tileSize * (Mathf.Abs(yInput) / yInput), 0f);
                }
            }
            transform.position = Vector3.MoveTowards(transform.position, destination, moveSpeed * Time.deltaTime);
            if (Vector3.Distance(transform.position, destination) <= Vector3.Distance(transform.position, occupiedTile)) {
                occupiedTile = destination;
            }
        }
    }
}
