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

    public List<Partygoer> partygoers;
    public PartySettings defaultParty;
    void Awake(){
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

        GenerateParty(defaultParty);
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
    }

    public void GenerateParty(PartySettings settings){
        Debug.Log("Generating party.");
        ClearParty();
        LightManager.instance.Randomize();
        GeneratePartygoers(settings);
    }

    void GeneratePartygoers(PartySettings settings){
        int partyGoerCount = settings.GeneratePartygoerCount();
        for(int i = 0; i < partyGoerCount; i++){
            SpawnPartyGoer(settings.partyGoerPrefab);
        }
    }

    void SpawnPartyGoer(GameObject prefab){
        float xPos = UnityEngine.Random.Range(minBound.x, maxBound.x);
        float yPos = UnityEngine.Random.Range(minBound.y, maxBound.y);

        Vector3 pos = new Vector3(xPos, yPos, 0);
        pos = transform.TransformPoint(pos);

        Partygoer partygoer = Instantiate(prefab, pos, Util.RandomYEuler()).GetComponent<Partygoer>();
        partygoers.Add(partygoer);
    }
}
