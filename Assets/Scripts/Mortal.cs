using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerController;

public abstract class Mortal : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public abstract void Hit(PoopType poopType);
}
