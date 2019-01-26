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
    public Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();   
    }
    private void Update()
    {
        anim.SetFloat(Name, PartyFloor.normalizedBPM);
    }
}
