using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoopSpin : MonoBehaviour
{
    private float spinSpeed;
    // Start is called before the first frame update
    void Start()
    {
        spinSpeed = Random.Range(-5f, 5f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Rotate(0, 0, transform.rotation.z + spinSpeed);
    }
}
