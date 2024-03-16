using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectCollisions : MonoBehaviour
{
    private float boxSize;

    private IRetreatable mover;
    private BoxCollider2D boxCollider;

    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        mover = GetComponent<IRetreatable>();
        // assumes that the colliders are always square
        boxSize = boxCollider.size.x;
    }

    // Update is called once per frame
    void Update()
    {
        Collider2D[] colliders = Physics2D.OverlapBoxAll(transform.position, new Vector2(boxSize, boxSize), 0f);

        List<Collider2D> list = new();
        list.AddRange(colliders);
        list.Remove(boxCollider);
        Collider2D[] result = list.ToArray();

        if (LevelManager.containsSolidCollider(result))
        {
            mover.Retreat();
        }
    }
}