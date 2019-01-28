using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMusic : MonoBehaviour
{
    public static AudioSource source;
    // Start is called before the first frame update
    static bool audioOn;
    void Start()
    {
        source = GetComponent<AudioSource>();
        source.PlayScheduled(0);
    }

    public static void ToggleAudio(){
        audioOn = !audioOn;

        source.volume = audioOn ? 1 : 0;
    }
    
}
