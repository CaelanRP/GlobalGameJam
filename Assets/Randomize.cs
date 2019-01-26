using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Randomize : MonoBehaviour
{
    public bool randomRotation = true;
    public bool rotX = true;
    public bool rotY = true;
    public bool rotZ = true;
    public bool randomScale = true;
    public Vector2 scaleMinMax = new Vector2(0.5f, 1.5f);
    // Start is called before the first frame update
    void Awake()
    {
        float randX = 0;
        float randY = 0;
        float randZ = 0;
        if (rotX) randX = Random.Range(0, 360);
        if (rotY) randY = Random.Range(0, 360);
        if (rotZ) randZ = Random.Range(0, 360);
        transform.eulerAngles = new Vector3(randX, randY, randZ);
        transform.localScale *= Random.Range(scaleMinMax.x, scaleMinMax.y);
    }
    
}
