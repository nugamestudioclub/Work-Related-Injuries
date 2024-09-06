using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerInventory2 : MonoBehaviour
{
    // the initial velocity of the box after being thrown in units per second
    public float throwVelocity;

    private GameObject heldObject = null;
    private IPlayerMover mover;

    private bool pickupButtonDown = false;
    private bool throwButtonDown = false;

    private Image inventoryImage;

    private bool inventoryDisplay = true;

    // Start is called before the first frame update
    void Start()
    {
        mover = GetComponent<IPlayerMover>();

        Debug.Log(mover);

        GameObject inventoryPanel = GetComponent<PlayerIdentifier>().GetInventoryPanel();



        inventoryPanel.SetActive(true);
        inventoryImage = inventoryPanel.transform.GetChild(0).GetComponent<Image>();
        UpdateInventoryDisplay();
    }

    // Update is called once per frame
    void Update()
    {
        
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
            SFXManager.instance.PlayerPickup();
            UpdateInventoryDisplay();
        }
    }

    void AttemptPlace()
    {
        BoxCollider2D collider = heldObject.GetComponent<BoxCollider2D>();
        // collider.size as last argument
        if (LevelManager.TargetSpaceOpen((Vector2)(mover.GetForwardPoint()) + collider.offset, new Vector2(0.1f, 0.1f)))
        {
            heldObject.transform.position = mover.GetForwardPoint();
            heldObject.SetActive(true);
            heldObject = null;
            SFXManager.instance.PlayerPlace();
            UpdateInventoryDisplay();
        }
    }

    void AttemptThrow()
    {
        BoxCollider2D collider = heldObject.GetComponent<BoxCollider2D>();
        if (LevelManager.TargetSpaceOpen((Vector2)(mover.GetForwardPoint()) + collider.offset, new Vector2(0.1f, 0.1f)))
        {
            heldObject.transform.position = mover.GetForwardPoint();
            heldObject.SetActive(true);
            heldObject.GetComponent<Rigidbody2D>().velocity = mover.GetForwardDirection() * throwVelocity;
            heldObject = null;
            SFXManager.instance.PlayerThrow();
            UpdateInventoryDisplay();
        }
    }

    public void OnPickup(InputAction.CallbackContext ctx)
    {
        bool buttonState = ctx.ReadValueAsButton();

        if (!pickupButtonDown)
        {
            if (buttonState)
            {
                if (heldObject == null)
                {
                    AttemptPickup();
                }
                else
                {
                    AttemptPlace();
                }
                pickupButtonDown = true;
            }
        }
        else
        {
            if (!buttonState)
            {
                pickupButtonDown = false;
            }
        }
    }

    public void OnThrow(InputAction.CallbackContext ctx)
    {
        bool buttonState = ctx.ReadValueAsButton();

        if (!throwButtonDown)
        {
            if (buttonState)
            {
                if (heldObject == null)
                {
                    AttemptPickup();
                }
                else
                {
                    AttemptThrow();
                }
                throwButtonDown = true;
            }
        }
        else
        {
            if (!buttonState)
            {
                throwButtonDown = false;
            }
        }
    }

    public void DestroyHeldObject()
    {
        if (heldObject != null)
        {
            heldObject.GetComponent<BoxCollisions>().DestroyBox(false);
            heldObject = null;
        }
        UpdateInventoryDisplay();
    }

    private void UpdateInventoryDisplay()
    {
        if (heldObject == null)
        {
            inventoryImage.sprite = null;
            inventoryImage.color = new Color(1f, 1f, 1f, 0f);
        }
        else
        {
            inventoryImage.sprite = heldObject.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite;
            inventoryImage.color = new Color(1f, 1f, 1f, 1f);
        }
    }
}
