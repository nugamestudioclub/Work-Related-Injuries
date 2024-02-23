using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    // in meters
    public float maxDistance;
    // in meters per second
    public float chargeRate;
    // in meters per second
    public float depleteRate;

    private GameObject heldObject = null;

    private float chargedDistance;

    private PlayerMover mover;

    private bool pickupOver = true;

    // Start is called before the first frame update
    void Start()
    {
        mover = this.gameObject.GetComponent<PlayerMover>();
        chargedDistance = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.E) && !pickupOver)
        {
            pickupOver = true;
        }

        if (heldObject == null)
        {
            if (Input.GetKeyDown(KeyCode.E) && mover.onTileCenter())
            {
                Collider2D[] colliders = Physics2D.OverlapBoxAll(mover.GetForwardPoint(), new Vector2(0.8f, 0.8f), 0f);
                foreach (Collider2D collider in colliders)
                {
                    if (collider.gameObject.name == "Box")
                    {
                        heldObject = collider.gameObject;
                        collider.gameObject.SetActive(false);
                        pickupOver = false;
                        break;
                    }
                }
            }
        }
        else
        {
            if (Input.GetKey(KeyCode.E) && mover.onTileCenter() && mover.targetTileOpen(mover.GetForwardPoint()) && pickupOver)
            {
                chargedDistance = Mathf.MoveTowards(chargedDistance, maxDistance, chargeRate * Time.deltaTime);
            }
            else
            {
                if (chargedDistance > 0f)
                {
                    int throwDistance = Mathf.RoundToInt(chargedDistance);
                    chargedDistance = 0f;

                    if (throwDistance == 0)
                    {
                        throwDistance = 1;
                    }

                    heldObject.transform.position = transform.position + mover.GetForwardDisplacement() * throwDistance;
                    heldObject.SetActive(true);
                    heldObject = null;
                }
            }
        }
    }
}
