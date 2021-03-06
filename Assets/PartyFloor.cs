﻿using System;
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
    public static List<Partygoer> partygoers = new List<Partygoer>();
    [HideInInspector]
    public static List<PartyProp> props = new List<PartyProp>();
    public List<PartySettings> standardParties;
    private PartySettings currentParty;
    public List<PartySettings> rareParties;

    public List<Song> songs;

    public static float normalizedBPM
    {
        get
        {
            if (instance.currentSong != null)
            return instance.currentSong.BPM / 120.0f;

            return 1;
        }
    }

    [HideInInspector]
    public Song currentSong;
    
    public static PartyFloor instance;
    public bool generateOnStart;

    public bool lightsOn;
    public float rarePartyChance;
    public int minBetweenRareParties;
    int partiesSinceRare;
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
        SwitchSong();
        if (generateOnStart){
            StartParty();
        }
        Speaker.thump += MaybeSwitchParty;
    }
    int beats = 0;
    void MaybeSwitchParty()
    {
        beats++;
        Debug.Log("Beats " + beats);
        if (beats >= 16)
        {
            beats = 0;
            StartParty();
        }
    }

    int song = 0;
    void Update(){
        if (input.GetButtonDown("LightSwitch")){
            StartParty();
        }
        if (Input.GetKeyDown(KeyCode.X)) 
        {
           // SwitchSong();
        }


        if (Input.GetKeyDown(KeyCode.Escape)) 
        {
            Application.Quit();
        }
    }
    public AudioSource source;
    void SwitchSong()
    {
        currentSong = songs[song];
        if (source.clip != currentSong.clip)
        {
            source.clip = currentSong.clip;
            source.Play();
        }
        song++;
        if (song >= songs.Count)
        {
            song = 0;
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

    public void StartParty(){
        SetParty();
        ClearParty();
        GenerateProps(currentParty);
        GenerateParty(currentParty);
        partiesSinceRare++;
    }

    public void GenerateParty(PartySettings settings){
        Debug.Log("Generating party.");
        LightManager.instance.Randomize();
        GeneratePartygoers(settings);
    }

    void GenerateProps(PartySettings settings){
        // Generate required props
        foreach(GameObject g in settings.requiredProps){
            PartyProp prop = g.GetComponent<PartyProp>().Spawn();
        }
        
        int propCount = settings.GeneratePropCount();
        List<GameObject> possibleProps = settings.GetRandomProps(propCount);
        for(int i = 0; i < propCount; i++){

            PartyProp prop = possibleProps[i].GetComponent<PartyProp>().Spawn();
            if (prop){
                //props.Add(prop);
            }
            /* 
            GameObject partyGoerPrefab = Util.RandomSelection<GameObject>(possiblePartyGoers, p => settings.DancerWeight(possiblePartyGoers.IndexOf(p)));
            SpawnPartyGoer(partyGoerPrefab);
            */
        }
    }

    void GeneratePartygoers(PartySettings settings){
        int num = 2;
        if (settings.standardDancers.Count > 0){
            List<GameObject> possiblePartyGoers = settings.GetRandomDancers(num);
            int[] dances = new int[num];
            // generate dance types
            for(int i = 0; i < dances.Length; i++){
                dances[i] = UnityEngine.Random.Range(0, 4);
            }

            int partyGoerCount = settings.GeneratePartygoerCount();
            for(int i = 0; i < partyGoerCount; i++){
                GameObject partyGoerPrefab;
                if (settings.rareDancers.Count > 0 && Util.random.NextDouble() < settings.rareDancerChance){
                    partyGoerPrefab = Util.RandomSelection<GameObject>(settings.rareDancers, d => 1);
                    SpawnPartyGoer(partyGoerPrefab);
                }
                else{
                    
                    partyGoerPrefab = Util.RandomSelection<GameObject>(possiblePartyGoers, p => settings.DancerWeight(possiblePartyGoers.IndexOf(p)));
                    SpawnPartyGoer(partyGoerPrefab, dances[possiblePartyGoers.IndexOf(partyGoerPrefab)]);
                }
                
            }
        }

        // Spawn required dudes
        foreach(Vector3 vec in settings.randomGuyLocations){
            SpawnPartyGoer(settings, vec);
        }
    }

    // Spawn literally anyone
    void SpawnPartyGoer(PartySettings settings, Vector3 pos){
        GameObject partyGoerPrefab;
                partyGoerPrefab = Util.RandomSelection<GameObject>(settings.standardDancers, d => 1);
                SpawnPartyGoer(partyGoerPrefab, pos); 
    }

    void SpawnPartyGoer(GameObject prefab, int dance = -1){
        float xPos = UnityEngine.Random.Range(minBound.x, maxBound.x);
        float yPos = UnityEngine.Random.Range(minBound.y, maxBound.y);

        Vector3 pos = new Vector3(xPos, yPos, 0);
        pos = transform.TransformPoint(pos);

        SpawnPartyGoer(prefab, pos, dance);
    }

    void SpawnPartyGoer(GameObject prefab, Vector3 pos, int dance = -1){

        RaycastHit hit;
        if (Physics.SphereCast(pos, 3f, Vector3.zero, out hit, 0.1f, PartyProp.LAYER)){
            return;
        }

        Partygoer partygoer = Instantiate(prefab, pos, Util.RandomYEuler()).GetComponent<Partygoer>();
        partygoer.danceType = dance;
        partygoers.Add(partygoer);
    }

    void SpawnProp(){

    }

    void ToggleLights(){
        if (lightsOn){
            LightsOff();
        }
        else{
            LightsOn();
        }
    }

    void LightsOff(){
        lightsOn = false;
        ClearParty();
        GenerateProps(currentParty);
    }

    void LightsOn(){
        lightsOn = true;
        GenerateParty(currentParty);
    }

    void SetParty() {
        if (rareParties.Count > 0 && partiesSinceRare > minBetweenRareParties && Util.random.NextDouble() < rarePartyChance) {
            currentParty = Util.RandomSelection(rareParties);
        }
        else {
            currentParty = Util.RandomSelection(standardParties);
        }
        
    }
}
