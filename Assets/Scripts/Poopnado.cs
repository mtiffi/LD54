using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerController;

public class Poopnado : MonoBehaviour
{
    private PoopType poopType;
    private Transform playerTransform;
    public float speed;
    public float timeToCircle;
    private Rigidbody2D rig;
    private bool rigOff;

    // Start is called before the first frame update
    void Start()
    {
        poopType = GetComponent<ProjectileHit>().pooptype;
        playerTransform = GameObject.Find("Player").transform;
        rig = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (poopType != PoopType.laser)
            return;

        timeToCircle -= Time.deltaTime;
        if (timeToCircle < 0)
        {
            if (!rigOff)
            {
                rig.velocity = new Vector2(0, 0);
                rig.isKinematic = true;
                rigOff = true;

            }
            transform.RotateAround(playerTransform.position, new Vector3(0, 0, 1), speed * Time.deltaTime);
        }

    }
}
