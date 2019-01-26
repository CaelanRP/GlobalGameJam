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
    [BoxGroup("Partygoers")]
    public GameObject partyGoerPrefab;
    

    public int GeneratePartygoerCount(){
        float f = Util.GetNormalDistFloat(partyGoerCountAvg, partyGoerCountStdDev, partyGoerCountMin, partyGoerCountMax);
        return Mathf.RoundToInt(f);
    }

    
}
