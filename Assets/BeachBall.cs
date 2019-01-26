using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeachBall : MonoBehaviour
{
    public float bumpForce = 10;
    public float gravity = 0.2f;
    Rigidbody r;
    // Start is called before the first frame update
    void Start()
    {
        r = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        r.AddForce( Physics.gravity * gravity);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.transform.CompareTag("Wall"))
        {
            r.AddForce(Vector3.up * bumpForce + Vector3.right * bumpForce * Random.Range(-0.5f, .5f) + Vector3.forward * bumpForce * Random.Range(-0.5f, .5f), ForceMode.Impulse);
        }
    }
}
