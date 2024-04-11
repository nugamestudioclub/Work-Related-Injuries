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
                rb.AddForce(new Vector2(beltForce * Time.deltaTime, 0));
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
            Invoke("Respawn", 2f);
        }
    }

    public void Respawn()
    {
        transform.position = respawner.position;

        isDead = false;
        mover.enabled = true;
        inventory.enabled = true;
        animator.enabled = true;
        transform.GetChild(0).gameObject.SetActive(true);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(boxCollider.bounds.min, boxCollider.bounds.max);
    }
}
