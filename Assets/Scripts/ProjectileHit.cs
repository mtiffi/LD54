using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using static PlayerController;

public class ProjectileHit : Mortal
{
    public GameObject poopShower;
    private Vector3 velocity, lastTransform;
    public PoopType pooptype = PoopType.normal;

    public bool hitWallOnce;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        lastTransform = transform.position;
        velocity = GetComponent<Rigidbody2D>().velocity;

    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.GetComponent<Mortal>())
        {
            if (!other.gameObject.GetComponent<ProjectileHit>() || other.gameObject.GetComponent<ProjectileHit>().pooptype != PoopType.big)
            {
                other.gameObject.GetComponent<Mortal>().Hit();
                GameObject poopShowerInstance = Instantiate(poopShower, lastTransform, Quaternion.LookRotation(velocity));
            }
            if (pooptype != PoopType.big)
                Hit();
        }
        if (other.gameObject.tag == "Wall")
        {
            hitWallOnce = true;
        }
        if (other.gameObject.tag == "Player" && hitWallOnce)
        {
            GameObject poopShowerInstance = Instantiate(poopShower, lastTransform, Quaternion.LookRotation(velocity));

        }
    }

    public override void Hit()
    {
        Invoke("Die", .0001f);
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
