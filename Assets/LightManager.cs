using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightManager : MonoBehaviour
{
    public static LightManager instance;

    private void Awake()
    {
        instance = this;
    }
    public Gradient ambientGradient;
    public Gradient backgroundGradient;
    Camera[] cameras;
    // Start is called before the first frame update
    void Start()
    {
        cameras = FindObjectsOfType<Camera>();
        
        

        Randomize();
    }

    // Update is called once per frame
    public void Randomize()
    {
        float randomColor = Random.value;
        float brightness = Random.value + .1f;
        RenderSettings.ambientLight = ambientGradient.Evaluate(randomColor) * brightness;
        for (int i = 0; i < cameras.Length; i++)
        {
            cameras[i].backgroundColor = backgroundGradient.Evaluate(randomColor);
        }
    }
    
}
