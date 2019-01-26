﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using NaughtyAttributes;

public class PartyProp : MonoBehaviour
{
    public Rigidbody rb;
    public Vector3 minSpawnPos, maxSpawnPos;
    void Awake(){
        rb = GetComponent<Rigidbody>();
    }

    public PartyProp Spawn(){
        PartyProp prop = Instantiate(gameObject, GetSpawnVector(), Quaternion.identity).GetComponent<PartyProp>();
        
        RaycastHit[] hits = rb.SweepTestAll(Vector3.up, 0.1f);
        int tries = 4;
        while(hits.Any(h => h.collider.GetComponent<PartyProp>())){
            prop.transform.position = GetSpawnVector();
            hits = rb.SweepTestAll(Vector3.up, 0.1f);
            tries --;
            if (tries == 0){
                Destroy(prop.gameObject);
                return null;
            }
        }
        return prop;
    }

    public Vector3 GetSpawnVector(){
        float x = UnityEngine.Random.Range(minSpawnPos.x, maxSpawnPos.x);
        float y = UnityEngine.Random.Range(minSpawnPos.y, maxSpawnPos.y);
        float z = UnityEngine.Random.Range(minSpawnPos.z, maxSpawnPos.z);
        return new Vector3(x,y,z);
    }
}
