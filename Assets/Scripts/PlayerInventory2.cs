using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory2 : MonoBehaviour
{
    // the initial velocity of the box after being thrown in units per second
    public float throwVelocity;

    private GameObject heldObject = null;
    private IPlayerMover mover;

    // Start is called before the first frame update
    void Start()
    {
        mover = GetComponent<IPlayerMover>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (heldObject == null)
            {
                AttemptPickup();
            }
            else
            {
                AttemptPlace();
            }
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (heldObject == null)
            {
                AttemptPickup();
            }
            else
            {
                AttemptThrow();
            }
        }
    }

    void AttemptPickup()
    {
        Vector3 forwardPoint = mover.GetForwardPoint();
        Collider2D[] colliders = Physics2D.OverlapPointAll(forwardPoint);
        List<Collider2D> boxColliders = new List<Collider2D>();
        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Box"))
            {
                boxColliders.Add(collider);
            }
        }
        Collider2D closeCollider = null;
        float minDistance = Mathf.Infinity;
        foreach (Collider2D collider in boxColliders)
        {
            float curDistance = Vector3.Distance(forwardPoint, collider.gameObject.transform.position);
            if (curDistance < minDistance)
            {
                minDistance = curDistance;
                closeCollider = collider;
            }
        }
        if (closeCollider != null)
        {
            heldObject = closeCollider.gameObject;
            closeCollider.gameObject.SetActive(false);
        }
    }

    void AttemptPlace()
    {
        BoxCollider2D collider = heldObject.GetComponent<BoxCollider2D>();
        if (LevelManager.TargetSpaceOpen((Vector2)(mover.GetForwardPoint()) + collider.offset, collider.size))
        {
            heldObject.transform.position = mover.GetForwardPoint();
            heldObject.SetActive(true);
            heldObject = null;
        }
    }

    void AttemptThrow()
    {
        BoxCollider2D collider = heldObject.GetComponent<BoxCollider2D>();
        if (LevelManager.TargetSpaceOpen((Vector2)(mover.GetForwardPoint()) + collider.offset, collider.size))
        {
            heldObject.transform.position = mover.GetForwardPoint();
            heldObject.SetActive(true);
            heldObject.GetComponent<Rigidbody2D>().velocity = mover.GetForwardDirection() * throwVelocity;
            heldObject = null;
        }
    }
}
