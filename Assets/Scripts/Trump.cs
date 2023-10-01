using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using static PlayerController;

public class Trump : Mortal
{
    private bool hasHair = true, goingUp = true;
    private SpriteRenderer renderer;
    public GameObject hair, pooplosion;
    public Sprite withHair, withOutHair;
    private Transform playerTransform;
    public float shotSpeed, walkSpeed;

    public GameObject WinText;

    private PlayerController playerController;

    public float lives = 10;
    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
        playerTransform = GameObject.Find("Player").GetComponent<Transform>();
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (hasHair)
        {
            hasHair = false;
            Vector3 shootDirection = playerTransform.position - transform.position;

            GameObject hairInstance = GameObject.Instantiate(hair, transform.position, Quaternion.identity);
            hairInstance.GetComponent<Rigidbody2D>().velocity = new Vector2(shootDirection.x, shootDirection.y).normalized * shotSpeed;
            renderer.sprite = withOutHair;
        }
        if (goingUp && transform.position.y < 7)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + walkSpeed * Time.deltaTime, transform.position.z);
        }
        else if (goingUp && transform.position.y > 7)
        {
            goingUp = false;
        }
        if (!goingUp && transform.position.y > -7)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - walkSpeed * Time.deltaTime, transform.position.z);
        }
        else if (!goingUp && transform.position.y < -7)
        {
            goingUp = true;
        }

    }

    void RefreshHair()
    {
        hasHair = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Hair")
        {
            Destroy(other.gameObject);
            Invoke("RefreshHair", Random.Range(1f, 2f));
            renderer.sprite = withHair;

        }
    }

    public override void Hit(PoopType poopType)
    {
        if (poopType == PoopType.laser)
        {
            lives -= .1f;
        }
        else
            lives--;
        if (lives == 0)
        {
            Instantiate(pooplosion, transform.position, Quaternion.identity);
            Destroy(gameObject);
            if (playerController.lives > 0)
            {
                WinText.SetActive(true);
                playerController.won = true;
            }

        }
    }
}
