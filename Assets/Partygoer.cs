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
    Animator animator;
    public string danceTrigger;

    void Awake(){
        animator = GetComponent<Animator>();
        if (!string.IsNullOrEmpty(danceTrigger)){
            animator.SetTrigger(danceTrigger);
        }
    }

    void Start(){
        PartyFloor.partygoers.Add(this);
    }
}
