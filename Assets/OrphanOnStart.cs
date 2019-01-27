using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using NaughtyAttributes;

public class OrphanOnStart : MonoBehaviour
{
    void Start(){
        transform.SetParent(null,true);
    }
}
