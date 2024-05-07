using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour
{
    public string[] levelNames;

    public void LoadLevel(int number)
    {
        if (number < levelNames.Length)
        {
            SceneManager.LoadScene(levelNames[number]);
        }
    }
}
