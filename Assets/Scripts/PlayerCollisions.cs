using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisions : MonoBehaviour
{
    public Vector2 centerOffset = new Vector2(0f, -0.2f);

    public Transform respawner;

    // the max speed the player can move and still fall into a hole in units per seconds
    public float maxSinkSpeed;

    // the amount of force a conveyor belt applies to a box
    public float beltForce;

    private PlayerMover2 mover;
    private PlayerInventory2 inventory;
    private PlayerAnimator animator;

    private BoxCollider2D boxCollider;
    private Rigidbody2D rb;

    private bool isDead = false;
    private bool respawnReady = false;

    private void Start()
    {
        mover = GetComponent<PlayerMover2>();
        inventory = GetComponent<PlayerInventory2>();
        animator = GetComponent<PlayerAnimator>();

        boxCollider = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();

        respawner = GameObject.FindGameObjectWithTag("Player1Respawn").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead)
        {
            return;
        }

        if (respawnReady)
        {
            if (LevelManager.BoundsOpen(boxCollider.bounds))
            {
                Respawn();
            }
        }

        Collider2D[] colliders = Physics2D.OverlapPointAll((Vector2)transform.position + centerOffset);

        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Saw"))
            {
                KillPlayer();
                break;
            }
            if (collider.CompareTag("Hole")) {
                if (collider.OverlapPoint(boxCollider.bounds.min) && collider.OverlapPoint(boxCollider.bounds.max))
                {
                    mover.grounded = false;
                    if (rb.velocity.magnitude <= maxSinkSpeed)
                    {
                        KillPlayer();
                        break;
                    }
                }
            }
            if (collider.CompareTag("RightBelt"))
            {
                rb.AddForce(new Vector2(beltForce, 0));
            }
            if (collider.CompareTag("LeftBelt"))
            {
                rb.AddForce(new Vector2(-beltForce, 0));
            }
            if (collider.CompareTag("UpBelt"))
            {
                rb.AddForce(new Vector2(0f, beltForce));
            }
            if (collider.CompareTag("DownBelt"))
            {
                rb.AddForce(new Vector2(0f, -beltForce));
            }
        }
    }

    public void KillPlayer()
    {
        if (!isDead)
        {
            isDead = true;
            mover.enabled = false;
            inventory.DestroyHeldObject();
            inventory.enabled = false;
            animator.enabled = false;
            transform.GetChild(0).gameObject.SetActive(false);
            boxCollider.enabled = false;
            Invoke("ReadyRespawn", 2f);
        }
    }

    public void ReadyRespawn()
    {
        transform.position = respawner.position;
        respawnReady = true;
        isDead = false;
    }

    public void Respawn()
    {
        mover.enabled = true;
        inventory.enabled = true;
        animator.enabled = true;
        transform.GetChild(0).gameObject.SetActive(true);
        boxCollider.enabled = true;
        respawnReady = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(boxCollider.bounds.min, boxCollider.bounds.max);
    }
}
