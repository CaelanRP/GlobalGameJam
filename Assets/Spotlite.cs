using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spotlite : MonoBehaviour
{
    //pendulum
    Light lite;
    float rotation;
    // Start is called before the first frame update
    void Awake()
    {
        lite = GetComponentInChildren<Light>();
        Randomize();
    }

    public void Randomize(float spotAngle)
    {
        rotation = Random.Range(-360, 360);
        lite.spotAngle = spotAngle;
    }

    public void Randomize()
    {
        rotation = Random.Range(-360, 360);
        lite.spotAngle = Random.Range(10, 100);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, rotation * Time.deltaTime, 0);
        lite.color = LightManager.instance.ambientGradient.Evaluate(Time.time % 1);
    }
}
