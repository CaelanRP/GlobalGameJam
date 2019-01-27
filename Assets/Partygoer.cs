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
    public string danceTrigger;

    protected override void Awake(){
        base.Awake();
        if (!string.IsNullOrEmpty(danceTrigger)){
            animator.SetTrigger(danceTrigger);
        }
    }

    void Start(){
        PartyFloor.partygoers.Add(this);
    }
}
