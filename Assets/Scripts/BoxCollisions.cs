using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxCollisions : MonoBehaviour
{
    public Vector2 centerOffset;

    // the max speed the box can move and still fall into a hole in units per seconds
    public float maxSinkSpeed;

    // the amount of force a conveyor belt applies to a box
    public float beltForce;
    // the amount of drag a box should experience on a belt when too fast
    public float beltDrag;
    // the min speed a box will experience belt drag
    public float maxBeltSpeed;
    // the amount that a box is displaced on an ideal horizontal belt
    public float horizontalBeltCenterOffset = -0.1f;
    // the amount of force a belt can exert to center a box
    public float beltCenterForce;

    private BoxCollider2D boxCollider;
    private Rigidbody2D rb;

    private float startDrag;

    // Start is called before the first frame update
    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        startDrag = rb.drag;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (rb.drag != startDrag)
        {
            rb.drag = startDrag;
        }

        Collider2D[] colliders = Physics2D.OverlapPointAll((Vector2)transform.position + centerOffset);

        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Saw"))
            {
                DestroyBox();
                break;
            }

            if (collider.CompareTag("Receptacle") && collider.TryGetComponent<ReceptacleController>(out var controller))
            {
                if (controller.ProcessBox(gameObject)) 
                {
                    LevelManager.Instance.ModifyScore(1);
                    DestroyBox(true);
                }

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
            if (collider.CompareTag("RightBelt"))
            {
                rb.AddForce(new Vector2(beltForce, 0));

                float tilesUp = (transform.position.y - LevelManager.gridOffset.y + horizontalBeltCenterOffset) / LevelManager.tileSize;
                int wholeTilesUp = (int)Mathf.Round(tilesUp);
                if (tilesUp >= wholeTilesUp)
                {
                    rb.AddForce(new Vector2(0f, -beltCenterForce));
                }
                else if (tilesUp <= wholeTilesUp)
                {
                    rb.AddForce(new Vector2(0f, beltCenterForce));
                }


                if (rb.velocity.magnitude > maxBeltSpeed)
                {
                    rb.drag = beltDrag;
                }
                break;
            }
            if (collider.CompareTag("LeftBelt"))
            {
                rb.AddForce(new Vector2(-beltForce, 0));

                float tilesUp = (transform.position.y - LevelManager.gridOffset.y + horizontalBeltCenterOffset) / LevelManager.tileSize;
                int wholeTilesUp = (int)Mathf.Round(tilesUp);
                if (tilesUp >= wholeTilesUp)
                {
                    rb.AddForce(new Vector2(0f, -beltCenterForce));
                }
                else if (tilesUp <= wholeTilesUp)
                {
                    rb.AddForce(new Vector2(0f, beltCenterForce));
                }

                if (rb.velocity.magnitude > maxBeltSpeed)
                {
                    rb.drag = beltDrag;
                }
                break;
            }
            if (collider.CompareTag("UpBelt"))
            {
                rb.AddForce(new Vector2(0f, beltForce));

                float tilesRight = (transform.position.x - LevelManager.gridOffset.x) / LevelManager.tileSize;
                int wholeTilesRight = (int)Mathf.Round(tilesRight);
                if (tilesRight >= wholeTilesRight)
                {
                    rb.AddForce(new Vector2(-beltCenterForce, 0f));
                }
                else if (tilesRight <= wholeTilesRight)
                {
                    rb.AddForce(new Vector2(beltCenterForce, 0f));
                }

                if (rb.velocity.magnitude > maxBeltSpeed)
                {
                    rb.drag = beltDrag;
                }
                break;
            }
            if (collider.CompareTag("DownBelt"))
            {
                rb.AddForce(new Vector2(0f, -beltForce));

                float tilesRight = (transform.position.x - LevelManager.gridOffset.x) / LevelManager.tileSize;
                int wholeTilesRight = (int)Mathf.Round(tilesRight);
                if (tilesRight >= wholeTilesRight)
                {
                    rb.AddForce(new Vector2(-beltCenterForce, 0f));
                }
                else if (tilesRight <= wholeTilesRight)
                {
                    rb.AddForce(new Vector2(beltCenterForce, 0f));
                }

                if (rb.velocity.magnitude > maxBeltSpeed)
                {
                    rb.drag = beltDrag;
                }
                break;
            }
        }
    }

    public void DestroyBox(bool properly_destroyed = false)
    {
        if (!properly_destroyed)
        {
            //LevelManager.Instance.ModifyScore(-1);
        }

        Destroy(gameObject);
    }
}
