using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using NaughtyAttributes;

public class SubProp : MonoBehaviour
{
    public List<GameObject> possibleProps;
    public float propChance;
    void OnDrawGizmos(){
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, 0.3f);
    }

    void Awake(){
        if (possibleProps.Count > 0 && Util.random.NextDouble() < propChance){
            GameObject obj = Util.RandomSelection(possibleProps);
            Instantiate(obj,transform.position, obj.transform.rotation);
        }
        //Destroy(gameObject);
    }
}
