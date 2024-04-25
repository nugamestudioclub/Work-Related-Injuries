using System.Collections.Generic;
using UnityEngine;

public class BoxOrientationManager : MonoBehaviour
{
    public enum BoxOrientation
    {
        Up, Down, Left, Right
    }

    [SerializeField]
    private List<SerializableItemPair<BoxOrientation, Sprite>> boxSprites;

    private BoxOrientation orientation;

    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    public void SetOrientation(BoxOrientation orientation)
    {
        this.orientation = orientation;

        MatchOrientation();
    }


    public BoxOrientation debug() => orientation;

    private void MatchOrientation()
    {
        foreach (var pair in boxSprites)
        {
            if (pair.Item1 == orientation)
            {
                spriteRenderer.sprite = pair.Item2;
                return;
            }
        }
    }

    public bool IsOfOrientation(BoxOrientation orientation) => this.orientation == orientation;
}
