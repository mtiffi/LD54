using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemys : MonoBehaviour
{
    public GameObject enemy1;
    public float maxX, maxY;

    public float spawnTimer;
    private float timer;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > spawnTimer)
        {
            timer = 0;
            Instantiate(enemy1, new Vector3(Random.Range(-maxX, maxX), Random.Range(-maxY, maxY), 0), Quaternion.identity);
        }
    }
}
