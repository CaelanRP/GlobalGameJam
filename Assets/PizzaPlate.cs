using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using NaughtyAttributes;

public class PizzaPlate : MonoBehaviour
{
    public List<GameObject> slices;

    void Awake(){
        int numSlices = UnityEngine.Random.Range(1,slices.Count);
        List<GameObject> slicesToRemove = Util.RandomSelections(slices, slices.Count - numSlices);
        foreach(GameObject obj in slicesToRemove){
            Destroy(obj);
        }
    }
}
