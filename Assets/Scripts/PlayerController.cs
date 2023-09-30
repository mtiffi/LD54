using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public enum PoopType
    {
        normal,
        spray,
        big
    }
    public float speed;
    public float shotSpeed;
    private Rigidbody2D rig;
    public GameObject poopPrefab;
    public PoopType currentPoopType = PoopType.normal;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            Shoot();
    }

    private void FixedUpdate()
    {
        Move();
    }

    void Shoot()
    {
        Vector3 shootDirection;
        shootDirection = Input.mousePosition;
        shootDirection.z = 0.0f;
        shootDirection = Camera.main.ScreenToWorldPoint(shootDirection);
        shootDirection = shootDirection - transform.position;

        switch (currentPoopType)
        {
            case PoopType.spray:
                shootDirection = Quaternion.AngleAxis(-45, Vector3.forward) * shootDirection;
                for (int i = 0; i < 10; i++)
                {
                    GameObject sprayPoopInstance = Instantiate(poopPrefab, transform.position, Quaternion.Euler(new Vector3(0, 0, 0)));
                    sprayPoopInstance.transform.localScale = new Vector3(1, 1, 1);
                    sprayPoopInstance.GetComponent<Rigidbody2D>().velocity = new Vector2(shootDirection.x, shootDirection.y).normalized * shotSpeed;
                    shootDirection = Quaternion.AngleAxis(10, Vector3.forward) * shootDirection;
                    sprayPoopInstance.GetComponent<ProjectileHit>().pooptype = PoopType.spray;

                }
                break;
            case PoopType.big:
                GameObject bigPoopInstance = Instantiate(poopPrefab, transform.position, Quaternion.Euler(new Vector3(0, 0, 0)));
                bigPoopInstance.transform.localScale = new Vector3(10, 10, 1);
                bigPoopInstance.GetComponent<Rigidbody2D>().velocity = new Vector2(shootDirection.x, shootDirection.y).normalized * shotSpeed / 2;
                bigPoopInstance.GetComponent<ProjectileHit>().pooptype = PoopType.big;

                break;

            default:
                GameObject poopInstance = Instantiate(poopPrefab, transform.position, Quaternion.Euler(new Vector3(0, 0, 0)));
                poopInstance.transform.localScale = new Vector3(3, 3, 1);
                poopInstance.GetComponent<Rigidbody2D>().velocity = new Vector2(shootDirection.x, shootDirection.y).normalized * shotSpeed;
                poopInstance.GetComponent<ProjectileHit>().pooptype = PoopType.normal;

                break;
        }
    }

    void Move()
    {
        Vector2 velocity = new Vector2();

        if (Input.GetAxis("Vertical") != 0)
        {
            velocity.y = Input.GetAxis("Vertical");
            if (Input.GetAxis("Vertical") < 0)
                anim.SetInteger("Direction", 0);
            else
                anim.SetInteger("Direction", 2);
        }
        if (Input.GetAxis("Horizontal") != 0)
        {
            velocity.x = Input.GetAxis("Horizontal");
            if (Input.GetAxis("Horizontal") < 0)
                anim.SetInteger("Direction", 3);
            else
                anim.SetInteger("Direction", 1);
        }
        if (Input.GetAxis("Vertical") == 0 && Input.GetAxis("Horizontal") == 0)
        {
            anim.speed = 0;
        }
        else
        {
            anim.speed = 1;
        }

        rig.velocity = velocity.normalized * speed;
    }
}
