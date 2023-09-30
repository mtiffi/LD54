using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnEnemys : MonoBehaviour
{
    public GameObject enemy1, chilli;
    private Transform playerTransform;
    public float maxX, maxY;

    public float spawnTimer;

    public float minDistanceToPlayer;
    private float timer;
    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameObject.Find("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > spawnTimer)
        {
            timer = 0;

            Instantiate(enemy1, GetPositionForObject(), Quaternion.identity);
            Instantiate(chilli, GetPositionForObject(), Quaternion.identity);
        }
    }

    private Vector3 GetPositionForObject()
    {
        Vector3 objectPosition = new Vector3(Random.Range(-maxX, maxX), Random.Range(-maxY, maxY), 0);
        if (Vector3.Distance(objectPosition, playerTransform.position) > minDistanceToPlayer)
        {
            return objectPosition;
        }
        else return GetPositionForObject();
    }
}
