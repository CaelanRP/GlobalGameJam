using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using NaughtyAttributes;

public class Hat : MonoBehaviour
{
    public List<GameObject> hats;
    int hatIndex = -1;

    public void SetHat(int index){
        for(int i = 0; i < hats.Count; i++){
            hats[i].SetActive(i == index);
        }
        hatIndex = index;
    }

    public void Start(){
        if (hatIndex == -1){
            if (Util.random.NextDouble() < 0.15f){
                RandomizeHat();
            }
        }
    }

    void RandomizeHat(){
        int index = UnityEngine.Random.Range(0,hats.Count);
        SetHat(index);
    }
}
