using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using NaughtyAttributes;

[CreateAssetMenu]
public class PartySettings : ScriptableObject
{
    [BoxGroup("Music")]
    public int BPM;
    [BoxGroup("Dancing")]
    public float danceIntensity;
    [BoxGroup("Props")]
    public float propCountAvg, propCountStdDev, propCountMin, propCountMax;
    [BoxGroup("Props")][ReorderableList]
    public List<GameObject> standardProps;
    [BoxGroup("Partygoers")]
    public float partyGoerCountAvg, partyGoerCountStdDev, partyGoerCountMin, partyGoerCountMax;
    [BoxGroup("Partygoers")][ReorderableList]
    public List<GameObject> standardDancers, rareDancers;
    [BoxGroup("Partygoers")][SerializeField]
    private List<int> dancerWeights;
    [BoxGroup("Partygoers")]
    public float rareDancerChance;

    public int GeneratePartygoerCount(){
        float f = Util.GetNormalDistFloat(partyGoerCountAvg, partyGoerCountStdDev, partyGoerCountMin, partyGoerCountMax);
        return Mathf.RoundToInt(f);
    }

    public int GeneratePropCount(){
        float f = Util.GetNormalDistFloat(propCountAvg, propCountStdDev, propCountMin, propCountMax);
        return Mathf.RoundToInt(f);
    }

    public int DancerWeight(int index){
        if (index > dancerWeights.Count-1){
            return 1;
        }
        return dancerWeights[index];
    }

    public List<GameObject> GetRandomDancers(int num){
        List<GameObject> temp = new List<GameObject>(standardDancers);
        List<GameObject> dancersToReturn = new List<GameObject>();
        while (num > 0){
            if (temp.Count == 0){
                temp = new List<GameObject>(standardDancers);
            }
            dancersToReturn.Add(temp[Util.random.Next(temp.Count)]);
            num--;
        }
        return dancersToReturn;
    }

    public List<GameObject> GetRandomProps(int num){
        List<GameObject> temp = new List<GameObject>(standardProps);
        List<GameObject> propsToReturn = new List<GameObject>();
        while (num > 0){
            if (temp.Count == 0){
                temp = new List<GameObject>(standardProps);
            }
            propsToReturn.Add(temp[Util.random.Next(temp.Count)]);
            num--;
        }
        return propsToReturn;
    }
}
