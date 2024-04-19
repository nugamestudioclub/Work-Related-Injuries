using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdentifier : MonoBehaviour
{
    public int playerNumber = -1;

    void Awake()
    {
        playerNumber = GameObject.FindGameObjectWithTag("PlayerSpawnManager").GetComponent<PlayerSpawner>().RegisterPlayer();
    }
}
