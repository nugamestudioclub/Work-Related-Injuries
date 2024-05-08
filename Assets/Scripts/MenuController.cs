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
        if (Time.timeScale != 1f) Time.timeScale = 1f;

        //Time.timeScale = 1f;

        SceneManager.LoadScene(levelLoad);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 1f;

            SceneManager.LoadScene(0);
        }
    }

    public void LoadStartScreen()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene(0);
    }
}
