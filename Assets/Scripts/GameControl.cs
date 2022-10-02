using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControl : MonoBehaviour
{
    public float Score = 0;
    public PlayerController playerControllerScript;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("ScreenScore", 0f, 0.1f);
         playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
     if (playerControllerScript.gameOver == false)
        {
            Score += playerControllerScript.moveSpeed * Time.deltaTime;
        }   
    }

    void ScreenScore()
    {
        if (playerControllerScript.gameOver == false)
        {
            Debug.Log($"Score: {Score.ToString("0")}");
        } 
        
    }
}
