using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBackground : MonoBehaviour
{
    Vector3 startPos;
    float repeatWidth;
    // Start is called before the first frame update
    void Start()
    {
        // Запоминаем стартовое положение
        startPos = transform.position;
        // Запоминаем длину повторяющегося участка фона
        repeatWidth = GetComponent<BoxCollider>().size.x / 2;
    }

    // Update is called once per frame
    void Update()
    {
        //Если сдвинулся фон на длину повторяющегося участка - 
        // возвращаем фон в стартовую точку
        if (transform.position.x < startPos.x - repeatWidth)
        {
            transform.position = startPos;
        }   
    }
}
