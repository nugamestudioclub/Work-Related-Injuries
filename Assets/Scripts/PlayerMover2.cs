using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMover2 : MonoBehaviour, IPlayerMover
{
    // the speed the player walks in units per second
    public float walkSpeed;
    // the speed the player runs in units per second
    public float sprintSpeed;

    // the current move speed of the player in units per second
    private float moveSpeed;

    // represents the current facing direction of the player
    private Orientation facing;

    private Rigidbody2D rb;


    void Start()
    {
        facing = Orientation.East;
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            moveSpeed = sprintSpeed;
        }
        else
        {
            moveSpeed = walkSpeed;
        }

        OrientPlayer();
    }

    private void FixedUpdate()
    {
        float xInput = Input.GetAxis("Horizontal");
        float yInput = Input.GetAxis("Vertical");

        Vector2 movementInput;


        /* Test with controller to see whether full control or right angle control is better
        if (Mathf.Abs(xInput) >= Mathf.Abs(yInput))
        {
            movementInput = new Vector2(xInput, 0f);
        } else
        {
            movementInput = new Vector2(0f, yInput);
        }
        */

        movementInput = new Vector2(xInput, yInput);

        movementInput = movementInput.normalized * moveSpeed;

        // TODO: note that this line causes the player's velocity to be set to 0 each frame when no input
        //rb.velocity = movementInput;

        rb.AddForce(movementInput);
    }

    private void OrientPlayer()
    {
        float xInput = Input.GetAxis("Horizontal");
        float yInput = Input.GetAxis("Vertical");

        if (Mathf.Abs(xInput) > Mathf.Abs(yInput))
        {
            if (xInput > 0f)
            {
                facing = Orientation.East;
            }
            if (xInput < 0f)
            {
                facing = Orientation.West;
            }
        }
        if (Mathf.Abs(yInput) > Mathf.Abs(xInput))
        {
            if (yInput > 0f)
            {
                facing = Orientation.North;
            }
            if (yInput < 0f)
            {
                facing = Orientation.South;
            }
        }
    }


    // returns the current player orientation
    public Orientation GetPlayerOrientation()
    {
        return facing;
    }

    // returns a Vector3 which represents the center point which is one
    // level unit away from the player in the direction the player is facing
    public Vector3 GetForwardPoint()
    {
        return transform.position + GetForwardDirection() * LevelManager.tileSize;
    }

    // returns a Vector3 which represents the unit vector corresponding to the
    // world space direction the player is facing
    public Vector3 GetForwardDirection()
    {
        switch (facing)
        {
            case Orientation.North:
                return Vector3.up;
            case Orientation.South:
                return Vector3.down;
            case Orientation.East:
                return Vector3.right;
            case Orientation.West:
                return Vector3.left;
            default:
                return Vector3.zero;
        }
    }
}
