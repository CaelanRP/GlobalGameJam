using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using NaughtyAttributes;
using Rewired;

public class PartyFloor : MonoBehaviour
{
    Vector2 minBound, maxBound;
    public float propMargin;

    Rewired.Player input;
    [HideInInspector]
    public List<Partygoer> partygoers;
    [HideInInspector]
    public List<PartyProp> props;
    public PartySettings defaultParty;
    
    public int bpm = 120;

    public static float normalizedBPM
    {
        get
        {
            return instance.bpm / 120.0f;
        }
    }
    
    public static PartyFloor instance;
    public bool generateOnStart;

    public GameObject testProp;
    void Awake(){
        instance = this;
        minBound = new Vector2(
            -0.5f + propMargin,
            -0.5f + propMargin
        );

        maxBound = new Vector2(
            0.5f- propMargin,
            0.5f - propMargin
        );

        input = ReInput.players.SystemPlayer;
    }
    private void Start()
    {
        if (generateOnStart){
            GenerateParty(defaultParty);
        }
    }
    void Update(){
        if (input.GetButtonDown("LightSwitch")){
            GenerateParty(defaultParty);

        }
    }

    void ClearParty(){
        while(partygoers.Count > 0){
            Destroy(partygoers[0].gameObject);
            partygoers.RemoveAt(0);
        }
        while(props.Count > 0){
            Destroy(props[0].gameObject);
            props.RemoveAt(0);
        }
    }

    public void GenerateParty(PartySettings settings){
        Debug.Log("Generating party.");
        ClearParty();
        LightManager.instance.Randomize();
        GenerateProps(settings);
        GeneratePartygoers(settings);
    }

    void GenerateProps(PartySettings settings){
        
        int propCount = settings.GeneratePropCount();
        List<GameObject> possibleProps = settings.GetRandomProps(propCount);
        for(int i = 0; i < propCount; i++){

            PartyProp prop = possibleProps[i].GetComponent<PartyProp>().Spawn();
            if (prop){
                props.Add(prop);
            }
            /* 
            GameObject partyGoerPrefab = Util.RandomSelection<GameObject>(possiblePartyGoers, p => settings.DancerWeight(possiblePartyGoers.IndexOf(p)));
            SpawnPartyGoer(partyGoerPrefab);
            */
        }
    }

    void GeneratePartygoers(PartySettings settings){
        List<GameObject> possiblePartyGoers = settings.GetRandomDancers(2);
        int partyGoerCount = settings.GeneratePartygoerCount();
        for(int i = 0; i < partyGoerCount; i++){
            GameObject partyGoerPrefab;
            if (Util.random.NextDouble() < settings.rareDancerChance){
                partyGoerPrefab = Util.RandomSelection<GameObject>(settings.rareDancers, d => 1);
            }
            else{
                partyGoerPrefab = Util.RandomSelection<GameObject>(possiblePartyGoers, p => settings.DancerWeight(possiblePartyGoers.IndexOf(p)));
            }
            SpawnPartyGoer(partyGoerPrefab);
        }
    }

    void SpawnPartyGoer(GameObject prefab){
        float xPos = UnityEngine.Random.Range(minBound.x, maxBound.x);
        float yPos = UnityEngine.Random.Range(minBound.y, maxBound.y);

        Vector3 pos = new Vector3(xPos, yPos, 0);
        pos = transform.TransformPoint(pos);

        RaycastHit hit;
        if (Physics.SphereCast(pos, 3f, Vector3.zero, out hit, 0.1f, PartyProp.LAYER)){
            return;
        }

        Partygoer partygoer = Instantiate(prefab, pos, Util.RandomYEuler()).GetComponent<Partygoer>();
        partygoers.Add(partygoer);
    }

    void SpawnProp(){

    }
}
