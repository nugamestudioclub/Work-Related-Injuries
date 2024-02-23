using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    public Sprite facingUp;
    public Sprite facingDown;
    public Sprite facingLeft;
    public Sprite facingRight;


    private SpriteRenderer render;
    private PlayerMover mover;

    // Start is called before the first frame update
    void Start()
    {
        mover = gameObject.GetComponent<PlayerMover>();
        render = gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Orientation facing = mover.GetPlayerOrientation();
        render.sprite = GetFacingSprite(facing);
    }

    private Sprite GetFacingSprite(Orientation facing)
    {
        switch (facing)
        {
            case Orientation.North:
                return facingUp;
            case Orientation.South:
                return facingDown;
            case Orientation.East:
                return facingRight;
            case Orientation.West:
                return facingLeft;
            default:
                return facingUp;
        }
    }
}
