using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisions : MonoBehaviour
{
    public Vector2 centerOffset = new Vector2(0f, -0.2f);

    public Transform respawner;

    private PlayerMover2 mover;
    private PlayerInventory2 inventory;
    private PlayerAnimator animator;

    private bool isDead = false;

    private void Start()
    {
        mover = GetComponent<PlayerMover2>();
        inventory = GetComponent<PlayerInventory2>();
        animator = GetComponent<PlayerAnimator>();

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
}
