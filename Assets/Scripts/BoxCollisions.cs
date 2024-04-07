using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxCollisions : MonoBehaviour
{
    public Vector2 centerOffset;

    // Start is called before the first frame update
    void Start()
    {
        
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
        }
    }

    public void DestroyBox()
    {
        Destroy(gameObject);
    }
}
