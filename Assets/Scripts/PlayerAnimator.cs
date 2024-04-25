using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    public Sprite[] redPlayer;
    public Sprite[] bluePlayer;
    public Sprite[] greenPlayer;
    public Sprite[] yellowPlayer;

    private Sprite facingUp;
    private Sprite facingDown;
    private Sprite facingLeft;
    private Sprite facingRight;


    private SpriteRenderer render;
    private IPlayerMover mover;

    // Start is called before the first frame update
    void Start()
    {
        mover = gameObject.GetComponent<IPlayerMover>();
        render = gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>();

        int playerNumber = GetComponent<PlayerIdentifier>().playerNumber;

        Sprite[][] allSprites = { redPlayer, bluePlayer, greenPlayer, yellowPlayer };

        facingUp = allSprites[playerNumber][0];
        facingDown = allSprites[playerNumber][1];
        facingLeft = allSprites[playerNumber][2];
        facingRight = allSprites[playerNumber][3];
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
