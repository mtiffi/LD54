using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerController;

public class EnemyDie : Mortal
{

    private AudioSource audioSource;
    public AudioClip dieClip;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void Hit(PoopType pooptype)
    {
        // audioSource.clip = dieClip;
        // audioSource.Play();
        GetComponent<PolygonCollider2D>().enabled = false;
        GetComponent<SpriteRenderer>().enabled = false;
        Invoke("Die", 1.5f);
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
