using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    //float speed = 30f;
    float deadLine = -5f;
    public PlayerController playerControllerScript;

    void Start()
    {

        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    void Update()
    {
        //Если игра не окончена - двигаемся влево
        if (playerControllerScript.gameOver == false)
        {
            transform.Translate(Vector3.left * playerControllerScript.moveSpeed * Time.deltaTime);
        }
        //Если сдвинулись вниз - уничтожаем объект
        if (transform.position.y < deadLine)
        {
            Destroy(gameObject);
        }
        
    }
}
