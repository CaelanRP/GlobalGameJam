using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirLight : MonoBehaviour
{
    Light lite;
    float rotation;
    void Start()
    {
        lite = GetComponentInChildren<Light>();
        Randomize();
    }

    public void Randomize(Color color)
    {
        rotation = Random.Range(-90, 90);
        lite.color = color;
    }

    public void Randomize()
    {
        rotation = Random.Range(-90, 90);
    }

    // Update is called once per frame
    void Update()
    {
        //transform.Rotate(0, rotation * Time.deltaTime, 0);
      //  lite.color = LightManager.instance.ambientGradient.Evaluate(Time.time % 1);
    }
}
