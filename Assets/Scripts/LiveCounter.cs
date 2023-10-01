using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LiveCounter : MonoBehaviour
{

    private Image image;
    private PlayerController playerController;
    public int liveCount;
    public Sprite empty, full;
    private bool emptyBool;
    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerController.lives >= liveCount)
        {
            if (emptyBool)
                image.sprite = full;
        }
        else if (!emptyBool)
        {
            image.sprite = empty;
        }
    }
}
