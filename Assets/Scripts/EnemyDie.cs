using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDie : Mortal
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void Hit()
    {
        Destroy(gameObject);
    }
}
