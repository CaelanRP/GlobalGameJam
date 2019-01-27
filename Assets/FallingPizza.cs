using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using NaughtyAttributes;

public class FallingPizza : MonoBehaviour
{
    public float fallDelayMin, fallDelayMax, fallChance;
    Rigidbody rb;

    void Start(){
        rb = GetComponent<Rigidbody>();
        StartCoroutine(PizzaFallRoutine());
    }

    IEnumerator PizzaFallRoutine(){
        float timeStarted = Time.time;
        bool willFall = Util.random.NextDouble() < fallChance;
        float fallDelay = UnityEngine.Random.Range(fallDelayMin, fallDelayMax);
        while(Time.time - timeStarted < fallDelay || !willFall){
            rb.velocity = Vector3.zero;
            yield return null;
        }
    }
}
