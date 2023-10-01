using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering.Universal.ShaderGUI;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightFlicker : MonoBehaviour
{
    private Light2D light2D;
    private bool flickering;

    private float timer;
    private float flickerTimer;
    public float timeBetweenFlickers, flickerTime;
    private float startIntensity;
    // Start is called before the first frame update
    void Start()
    {
        light2D = GetComponent<Light2D>();

        startIntensity = light2D.intensity;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (flickerTimer > flickerTime)
        {
            flickering = false;
            timer = 0;
            flickerTimer = 0;
        }
        if (timer > timeBetweenFlickers)
        {
            flickering = true;
        }
        if (!flickering)
        {
            light2D.intensity = startIntensity;
        }
        else
        {
            if (Time.frameCount % Random.Range(0, 20) == 0)
            {
                light2D.intensity = Random.Range(0f, .9f);
            }
            flickerTimer += Time.deltaTime;
        }
    }
}
