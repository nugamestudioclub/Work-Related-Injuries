using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectCollisions : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        Collider2D[] colliders = Physics2D.OverlapPointAll((Vector2)transform.position + new Vector2(0f, -0.2f));

        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Saw"))
            {
                Debug.Log("saw found this frame");
                break;
            }
        }
    }
}