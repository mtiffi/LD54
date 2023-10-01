using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Animations;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public enum PoopType
    {
        normal,
        spray,
        big,
        laser,
        slow
    }
    public float speed;
    public float shotSpeed;
    private Rigidbody2D rig;
    public GameObject poopPrefab, poopPrefab2, poopPrefab3, poopExplosion;
    public AudioClip poopnadoSound, slowSound, fart1, fartLong, fartShort, fartQuiet, ohShit;

    private GameObject[] poops = new GameObject[3];
    public PoopType currentPoopType = PoopType.normal;
    private Animator anim;

    public int lives = 3;

    public float chilli;
    private bool dead, pooping, lasering;
    private SpriteRenderer spriteRenderer;
    private AudioSource audioSource;

    public GameObject LooseText;

    public bool won;

    private float slowPoopTimer = 0, slowPoopTime = 2.5f;

    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        poops[0] = poopPrefab;
        poops[1] = poopPrefab2;
        poops[2] = poopPrefab3;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !dead && !pooping && !lasering && !won)
            Shoot(GetCurrentPoopType());
        if (pooping)
        {
            SlowPoop();
        }

        if (lasering)
        {
            Laser();
        }
    }

    private void FixedUpdate()
    {
        if (!dead && !pooping)
            Move();
        else
            rig.velocity = new Vector2(0, 0);


    }

    void SlowPoop()
    {
        slowPoopTimer += Time.deltaTime;
        spriteRenderer.color = Color.Lerp(Color.white, Color.red, slowPoopTimer / 4);
        if (slowPoopTimer > slowPoopTime)
        {
            slowPoopTimer = 0;
            pooping = false;
            Vector3 shootDirection = GetShootDirection();
            spriteRenderer.color = Color.white;
            GameObject bigPoopInstance = Instantiate(poopPrefab, transform.position, Quaternion.Euler(new Vector3(0, 0, 0)));
            bigPoopInstance.transform.localScale = new Vector3(.8f, .8f, 1);
            bigPoopInstance.GetComponent<Rigidbody2D>().velocity = new Vector2(shootDirection.x, shootDirection.y).normalized * shotSpeed / 2;
            bigPoopInstance.GetComponent<ProjectileHit>().pooptype = PoopType.normal;
        }
    }

    void Laser()
    {
        if (Time.frameCount % 10 == 0)
        {
            int randomPoop = Random.Range(0, 2);
            float randomSize = Random.Range(.3f, 2f);
            float randomXOffset = Random.Range(-.1f, .1f);
            float randomYOffset = Random.Range(-.1f, .1f);
            Vector3 shootDirection = GetShootDirection();
            GameObject poopInstance = Instantiate(poops[randomPoop], new Vector3(transform.position.x + randomXOffset, transform.position.y + randomYOffset, transform.position.z), Quaternion.Euler(new Vector3(0, 0, 0)));
            poopInstance.transform.localScale = new Vector3(randomSize, randomSize, 1);
            poopInstance.GetComponent<Rigidbody2D>().velocity = new Vector2(shootDirection.x, shootDirection.y).normalized * shotSpeed;
            poopInstance.GetComponent<ProjectileHit>().pooptype = PoopType.laser;
        }

    }

    Vector3 GetShootDirection()
    {
        Vector3 shootDirection;
        shootDirection = Input.mousePosition;
        shootDirection.z = 0.0f;
        shootDirection = Camera.main.ScreenToWorldPoint(shootDirection);
        shootDirection = shootDirection - transform.position;
        return shootDirection;
    }

    void Shoot(PoopType poopType)
    {
        Vector3 shootDirection = GetShootDirection();

        chilli--;
        if (chilli < 0)
            chilli = 0;

        switch (poopType)
        {
            case PoopType.spray:
                shootDirection = Quaternion.AngleAxis(-45, Vector3.forward) * shootDirection;
                for (int i = 0; i < 10; i++)
                {
                    GameObject sprayPoopInstance = Instantiate(poopPrefab2, transform.position, Quaternion.Euler(new Vector3(0, 0, 0)));
                    sprayPoopInstance.transform.localScale = new Vector3(.8f, .8f, .8f);
                    sprayPoopInstance.GetComponent<Rigidbody2D>().velocity = new Vector2(shootDirection.x, shootDirection.y).normalized * shotSpeed;
                    shootDirection = Quaternion.AngleAxis(10, Vector3.forward) * shootDirection;
                    sprayPoopInstance.GetComponent<ProjectileHit>().pooptype = PoopType.spray;
                    audioSource.clip = fartLong;

                }
                break;
            case PoopType.big:
                GameObject bigPoopInstance = Instantiate(poopPrefab, transform.position, Quaternion.Euler(new Vector3(0, 0, 0)));
                bigPoopInstance.transform.localScale = new Vector3(3, 3, 1);
                bigPoopInstance.GetComponent<Rigidbody2D>().velocity = new Vector2(shootDirection.x, shootDirection.y).normalized * shotSpeed / 2;
                bigPoopInstance.GetComponent<ProjectileHit>().pooptype = PoopType.big;
                audioSource.clip = fartQuiet;

                break;

            case PoopType.laser:

                for (int i = 0; i < 36; i++)
                {
                    GameObject sprayPoopInstance = Instantiate(poopPrefab, transform.position, Quaternion.Euler(new Vector3(0, 0, 0)));
                    sprayPoopInstance.transform.localScale = new Vector3(.8f, .8f, .8f);
                    sprayPoopInstance.GetComponent<Rigidbody2D>().velocity = new Vector2(shootDirection.x, shootDirection.y).normalized * shotSpeed * Random.Range(.5f, 3f);
                    shootDirection = Quaternion.AngleAxis(10, Vector3.forward) * shootDirection;
                    sprayPoopInstance.GetComponent<ProjectileHit>().pooptype = PoopType.laser;
                    sprayPoopInstance.transform.parent = transform;
                }
                audioSource.clip = poopnadoSound;
                break;
            case PoopType.slow:
                pooping = true;
                audioSource.clip = slowSound;
                break;

            default:
                GameObject poopInstance = Instantiate(poopPrefab, transform.position, Quaternion.Euler(new Vector3(0, 0, 0)));
                poopInstance.transform.localScale = new Vector3(1, 1, 1);
                poopInstance.GetComponent<Rigidbody2D>().velocity = new Vector2(shootDirection.x, shootDirection.y).normalized * shotSpeed;
                poopInstance.GetComponent<ProjectileHit>().pooptype = PoopType.normal;
                audioSource.clip = fart1;

                break;
        }
        audioSource.Play();

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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Chilli")
        {
            if (chilli + 1 < 10)
                chilli += 1;
            else chilli = 10;
            Destroy(other.gameObject);
        }
        if (other.tag == "Hair")
        {
            Hit();
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Enemy" || other.gameObject.tag == "Projectile")
        {
            if ((other.gameObject.tag == "Projectile" && other.gameObject.GetComponent<ProjectileHit>().hitWallOnce) || other.gameObject.tag == "Enemy")
            {
                other.gameObject.GetComponent<Mortal>().Hit();
                Hit();

            }
        }
    }

    private void Hit()
    {
        if (won) return;
        lives--;
        if (lives == 0)
        {
            audioSource.clip = ohShit;
            audioSource.Play();

            GameObject.Instantiate(poopExplosion, transform.position, Quaternion.identity);

            dead = true;
            GetComponent<PolygonCollider2D>().enabled = false;
            GetComponent<SpriteRenderer>().enabled = false;
            LooseText.SetActive(true);
        }
    }

    private PoopType GetCurrentPoopType()
    {
        if (chilli == 0)
        {
            return PoopType.slow;
        }
        else
        if (chilli < 5)
        {
            return PoopType.normal;
        }
        else if (chilli < 8)
        {
            return PoopType.spray;
        }
        else if (chilli < 10)
        {
            return PoopType.big;
        }
        else if (chilli >= 10)
        {
            return PoopType.laser;
        }
        else return PoopType.slow;
    }

}
