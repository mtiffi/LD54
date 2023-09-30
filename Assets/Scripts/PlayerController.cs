using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float shotSpeed;
    private Rigidbody2D rig;
    public GameObject poopPrefab;

    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        if (Input.GetMouseButtonDown(0))
            Shoot();
    }

    void Shoot()
    {
        Vector3 shootDirection;
        shootDirection = Input.mousePosition;
        shootDirection.z = 0.0f;
        shootDirection = Camera.main.ScreenToWorldPoint(shootDirection);
        shootDirection = shootDirection - transform.position;
        shootDirection = shootDirection.normalized;
        //...instantiating the rocket
        GameObject poopInstance = Instantiate(poopPrefab, transform.position, Quaternion.Euler(new Vector3(0, 0, 0)));
        poopInstance.GetComponent<Rigidbody2D>().velocity = new Vector2(shootDirection.x * shotSpeed, shootDirection.y * shotSpeed);
    }

    void Move()
    {
        Vector2 velocity = new Vector2();
        if (Input.GetAxis("Horizontal") != 0)
        {
            velocity.x = Input.GetAxis("Horizontal");
        }
        if (Input.GetAxis("Vertical") != 0)
        {
            velocity.y = Input.GetAxis("Vertical");
        }
        rig.velocity = velocity * speed;
    }
}
