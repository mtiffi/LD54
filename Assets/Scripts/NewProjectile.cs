using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewProjectile : MonoBehaviour
{
    private float time = 0.2f;
    private bool isProjectile;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!isProjectile)
        {
            time -= Time.deltaTime;
            if (time < 0)
            {
                if (GetComponent<ProjectileHit>().pooptype != PlayerController.PoopType.laser)
                    gameObject.layer = LayerMask.NameToLayer("Projectile");
                isProjectile = true;
            }
        }
    }
}
