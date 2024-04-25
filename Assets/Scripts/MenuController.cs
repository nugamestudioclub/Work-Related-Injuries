using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [SerializeField]
    private string levelLoad = "Level01";

    public void LoadLevel()
    {
        SceneManager.LoadScene(levelLoad);

        if (Time.timeScale != 1f) Time.timeScale = 1f;
    }
}
