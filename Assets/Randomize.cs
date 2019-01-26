using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class Randomize : MonoBehaviour
{
    public bool randomRotation = true;
    [ShowIf("randomRotation")]
    public bool rotX = true;
    [ShowIf("randomRotation")]
    public bool rotY = true;
    [ShowIf("randomRotation")]
    public bool rotZ = true;
    public bool randomScale = true;
    [ShowIf("randomRotation")]
    public Vector2 scaleMinMax = new Vector2(0.5f, 1.5f);
    // Start is called before the first frame update
    void Awake()
    {
        if (randomRotation){
            float randX = 0;
            float randY = 0;
            float randZ = 0;
            if (rotX) randX = Random.Range(0, 360);
            if (rotY) randY = Random.Range(0, 360);
            if (rotZ) randZ = Random.Range(0, 360);
            transform.eulerAngles = new Vector3(randX, randY, randZ);
        }
        if (randomScale){
            transform.localScale *= Random.Range(scaleMinMax.x, scaleMinMax.y);
        }
    }
    
}
