using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerController;

public class Hair : Mortal
{

    private bool goBack;
    private Rigidbody2D rig;
    private Transform donaldTransform;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        donaldTransform = GameObject.Find("donald").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (goBack)
        {
            rig.velocity = new Vector2(0, 0);
            transform.position = Vector3.MoveTowards(transform.position, donaldTransform.position, speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "Wall")
        {
            goBack = true;
            gameObject.layer = LayerMask.NameToLayer("Default");

        }
    }

    public override void Hit(PoopType pooptype)
    {
        goBack = true;
        gameObject.layer = LayerMask.NameToLayer("Hair");

    }
}
