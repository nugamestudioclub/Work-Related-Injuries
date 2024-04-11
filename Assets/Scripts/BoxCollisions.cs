using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxCollisions : MonoBehaviour
{
    public Vector2 centerOffset;

    // the max speed the box can move and still fall into a hole in units per seconds
    public float maxSinkSpeed;

    private BoxCollider2D boxCollider;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Collider2D[] colliders = Physics2D.OverlapPointAll((Vector2)transform.position + centerOffset);

        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Saw"))
            {
                DestroyBox();
                break;
            }

            if (collider.CompareTag("Hole"))
            {
                if (collider.OverlapPoint(boxCollider.bounds.min) && collider.OverlapPoint(boxCollider.bounds.max))
                {
                    if (rb.velocity.magnitude <= maxSinkSpeed)
                    {
                        DestroyBox();
                        break;
                    }
                }
            }
        }
    }

    public void DestroyBox()
    {
        Destroy(gameObject);
    }
}
