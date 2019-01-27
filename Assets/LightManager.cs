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
    public Camera[] cameras;
    public GameObject spotlights;
    public GameObject dirlights;
    // Start is called before the first frame update

    private void Start()
    {

        Randomize();
    }

    // Update is called once per frame
    public void Randomize()
    {
        float randomColor = Random.value;
        float randomColor2 = Random.value;
        float brightness = Random.value/2 + .5f;
        RenderSettings.ambientLight = ambientGradient.Evaluate(randomColor2) * brightness;
        for (int i = 0; i < cameras.Length; i++)
        {
            cameras[i].backgroundColor = backgroundGradient.Evaluate(randomColor);
        }
        float occurance = Random.value;
        if (occurance < 0.4f)
        {
            spotlights.SetActive(true);
            dirlights.SetActive(false);
            float spotAngle = Random.Range(10, 100);
            Spotlite[] spots = spotlights.GetComponentsInChildren<Spotlite>();
            int i = 0;
            foreach (Spotlite s in spots)
            {
                s.Randomize(spotAngle, ambientGradient.Evaluate((randomColor + (i * 0.2f)) % 1));
                i++;
                if (i >= 3)
                {
                    i = 0;
                }

            }
        }
        else if (occurance < 0.8f)
        {

            spotlights.SetActive(false);
            dirlights.SetActive(true);
            DirLight[] dirs = dirlights.GetComponentsInChildren<DirLight>();
            for (int i = 0; i < dirs.Length; i++)
            {
                dirs[i].Randomize(ambientGradient.Evaluate((randomColor + (i* 0.3f)) % 1));
            }
        }
        else
        {
            spotlights.SetActive(false);
            dirlights.SetActive(false);
            RenderSettings.ambientLight = ambientGradient.Evaluate(randomColor2);
        }
    }
    
}
