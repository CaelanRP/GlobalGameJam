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
    [BoxGroup("Partygoers")]
    public float partyGoerCountAvg, partyGoerCountStdDev, partyGoerCountMin, partyGoerCountMax;
    [BoxGroup("Partygoers")][ReorderableList]
    public List<GameObject> standardDancers;
    [BoxGroup("Partygoers")][SerializeField]
    private List<int> dancerWeights;

    public int GeneratePartygoerCount(){
        float f = Util.GetNormalDistFloat(partyGoerCountAvg, partyGoerCountStdDev, partyGoerCountMin, partyGoerCountMax);
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
}
