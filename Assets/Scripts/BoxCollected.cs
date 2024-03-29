using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxCollected : MonoBehaviour
{
    public int scoreValue = 1;
    public int totalScore = 0;
    public float destroyTime = 0.1f;
    GameObject boxToDestroy;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Triggered!");
        if (collision.gameObject.CompareTag("Box"))
        {
            boxToDestroy = collision.gameObject;
            Invoke("DestroyBox", destroyTime);
            totalScore += scoreValue;
        }
    }

    private void DestroyBox()
    {
        boxToDestroy.gameObject.SetActive(false);
    }
}
