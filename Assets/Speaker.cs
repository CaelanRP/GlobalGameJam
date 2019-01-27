using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class Speaker : MonoBehaviour
{
    public static UnityAction thump;
    public static Speaker mainSpeaker;
    private void Start()
    {
        mainSpeaker = this;

    }
    
    public void Thump()
    {
        if (this == mainSpeaker)
        {
            thump.Invoke();
        }
        else
        {
            Debug.Log("not main!");
        }
    }
    
}
