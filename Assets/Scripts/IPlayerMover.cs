using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayerMover
{
    Orientation GetPlayerOrientation();
    Vector3 GetForwardPoint();
    Vector3 GetForwardDirection();
}
