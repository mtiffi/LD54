using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private GameObject player;
    public float speed;

    private Rigidbody2D rig;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        rig = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 dir3d = player.transform.position - transform.position;
        Vector2 dir = new Vector2(dir3d.x, dir3d.y).normalized;
        rig.velocity = dir * speed;

    }
}
