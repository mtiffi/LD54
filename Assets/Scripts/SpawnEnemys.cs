using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnEnemys : MonoBehaviour
{
    public GameObject enemy1, chilli, donald;
    private Transform playerTransform;
    public float maxX, maxY;

    public TextMeshProUGUI textMeshProUGUI;

    private float time = 120;

    public float spawnTimer;

    public float minDistanceToPlayer;
    private float timer;

    private bool donaldSpawned;
    private float restartTimer;
    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameObject.Find("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.R))
        {
            restartTimer += Time.deltaTime;
            if (restartTimer > 1)
            {
                SceneManager.LoadScene("level1");
            }
        }
        else restartTimer = 0;
        timer += Time.deltaTime;
        time -= Time.deltaTime;
        if (time < 0) time = 0;
        textMeshProUGUI.text = TimeSpan.FromSeconds(time).ToString(@"m\:ss");
        if (!donaldSpawned && time <= 0)
        {
            Instantiate(donald, new Vector3(12, 0, 0), Quaternion.identity);
        }
        if (timer > spawnTimer && !donaldSpawned)
        {
            timer = 0;

            Instantiate(enemy1, GetPositionForObject(), Quaternion.identity);
            Instantiate(chilli, GetPositionForObject(), Quaternion.identity);
        }
    }

    private Vector3 GetPositionForObject()
    {
        Vector3 objectPosition = new Vector3(UnityEngine.Random.Range(-maxX, maxX), UnityEngine.Random.Range(-maxY, maxY), 0);
        if (Vector3.Distance(objectPosition, playerTransform.position) > minDistanceToPlayer)
        {
            return objectPosition;
        }
        else return GetPositionForObject();
    }
}
