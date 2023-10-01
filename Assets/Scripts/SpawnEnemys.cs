using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;

using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnEnemys : MonoBehaviour
{
    public GameObject enemy1, chilli, donald;
    private Transform playerTransform;
    public float maxX, maxY;

    public TextMeshProUGUI textMeshProUGUI;

    private float time = 60;

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
            donaldSpawned = true;
        }
        if (timer > spawnTimer)
        {
            timer = 0 + UnityEngine.Random.Range(-1f, 2f);
            if (!donaldSpawned)
            {
                Instantiate(enemy1, GetPositionForObject(), Quaternion.identity);
                if (UnityEngine.Random.Range(0f, 1f) <= 0.3f)
                    Instantiate(enemy1, GetPositionForObject(), Quaternion.identity);
                if (UnityEngine.Random.Range(0f, 1f) <= 1f)
                    Instantiate(chilli, GetPositionForObject(), Quaternion.identity);
            }
            if (UnityEngine.Random.Range(0f, 1f) <= 0.15f)
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
