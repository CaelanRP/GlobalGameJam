using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jiggle : MonoBehaviour
{
    public Collider body;
    public bool jiggle;
    public float intensity = 1;
    // Start is called before the first frame update
    void Start()
    {
        if (body)
        {
            Collider[] limbs = GetComponentsInChildren<Collider>();
            for (int i = 0; i < limbs.Length; i++)
            {
                Physics.IgnoreCollision(body, limbs[i]);
            }
        }
        if (jiggle)
        {
            StartCoroutine(JiggleRoutine());
        }
    }

    IEnumerator JiggleRoutine()
    {
        Rigidbody[] rigids = GetComponentsInChildren<Rigidbody>();
        //yield return new WaitForSeconds(.2f * Random.value);
        while (true)
        {
            for (int i = 0; i < rigids.Length; i++)
            {
                rigids[i].AddForce(Random.insideUnitSphere * Random.Range(50, 20)* intensity, ForceMode.Impulse);
            }
            yield return new WaitForSeconds(.5f);
        }
    }
    
}
