using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] obstaclePrefab;
    public float startDelay = 3f, repeatRate = 2f;
    public PlayerController playerControllerScript;
    float spawnTigger, dashTime = 0;

    Vector3 spawPos = new Vector3(25, 0, 0);
    // Start is called before the first frame update
    void Start()
    {
        // Запускаем спаун препятствий
        //InvokeRepeating("SpawnObstacle", startDelay, repeatRate);
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
        spawnTigger = Time.realtimeSinceStartup + Random.Range(1.5f, 2.5f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.realtimeSinceStartup + dashTime > spawnTigger)
        {
            SpawnObstacle();
            spawnTigger = Time.realtimeSinceStartup + Random.Range(1.5f, 2.5f);
            dashTime = 0;
        }
        if (playerControllerScript.moveSpeed > 35)
        {
            dashTime += Time.deltaTime * 0.5f;
        }
    }

    void SpawnObstacle()
    {
        int numObstacle = Random.Range(0, 4);
        // Если игра не окончена - спауним препятствие
        if (playerControllerScript.gameOver == false)
        {
            Instantiate(obstaclePrefab[numObstacle], spawPos, obstaclePrefab[numObstacle].transform.rotation);
        }
    }
}
