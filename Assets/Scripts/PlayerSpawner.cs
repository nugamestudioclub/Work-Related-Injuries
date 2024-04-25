using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;

public class PlayerSpawner : MonoBehaviour
{
    public GameObject playerPrefab;

    public GameObject[] spawners;

    public GameObject[] inventoryPanels;

    public int expectedPlayers;

    private int currentPlayers = 0;

    private void Start()
    {
        /*
        InputControlList<InputDevice> devices = InputUser.GetUnpairedInputDevices();
        Debug.Log("unparied devices = " + devices.Count);
        Debug.Log(gameObject.name);
        for (int i = 0; i < expectedPlayers; i++)
        {
            GetComponent<PlayerInputManager>().JoinPlayer(-1, -1, null, devices[i]);
        }
        */

        foreach (GameObject panel in inventoryPanels)
        {
            panel.SetActive(false);
        }
    }

    public int RegisterPlayer()
    {
        currentPlayers += 1;
        return currentPlayers - 1;
    }

    public Transform GetSpawner(int playerNumber)
    {
        return spawners[playerNumber].transform;
    }

    public GameObject GetInventoryPanel(int playerNumber)
    {
        return inventoryPanels[playerNumber];
    }
}
