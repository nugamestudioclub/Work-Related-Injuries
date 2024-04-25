using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdentifier : MonoBehaviour
{
    public int playerNumber = -1;

    private PlayerSpawner playerSpawner;

    void Awake()
    {
        playerSpawner = GameObject.FindGameObjectWithTag("PlayerSpawnManager").GetComponent<PlayerSpawner>();
        playerNumber = playerSpawner.RegisterPlayer();
    }

    public Transform GetSpawner()
    {
        return playerSpawner.GetSpawner(playerNumber);
    }

    public GameObject GetInventoryPanel()
    {
        return playerSpawner.GetInventoryPanel(playerNumber);
    }
}
