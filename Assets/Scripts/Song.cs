using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using NaughtyAttributes;
[CreateAssetMenu]

public class Song : ScriptableObject
{
    public AudioClip clip;
    public float BPM;
}
