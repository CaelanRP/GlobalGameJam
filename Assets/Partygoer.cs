using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using NaughtyAttributes;

public class Partygoer : PartyProp
{
    private const string Name = "speed";
    public Transform lookSpot;
    public int danceType = -1;

    protected override void Awake(){
        base.Awake();
    }

    void Start(){
        PartyFloor.partygoers.Add(this);
        int dance = danceType == -1 ? UnityEngine.Random.Range(0, 4) : danceType;
        animator.SetFloat("dance", danceType);
    }
}
