using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using NaughtyAttributes;
using Rewired;

public class GUIManager : MonoBehaviour
{
    float timePressed;
    public float showUITIme;
    Rewired.Player input;
    public float shortScroll, longScroll;
    public RectTransform textTransform;
    public Vector3 textVisiblePos, textHiddenPos;

    public static GUIManager instance;

    public TextMeshProUGUI BPMText;

    void Start(){
        input = ReInput.players.SystemPlayer;
        instance = this;
    }

    void Update(){
        float dir = Mathf.Sign(input.GetAxis("ScrollBPM"));
        if (input.GetButtonLongPress("ScrollBPM") || input.GetNegativeButtonLongPress("ScrollBPM")){
            PartyFloor.instance.currentBPM += Mathf.Sign(dir * longScroll);
            ShowUI();
        }
        else if (input.GetButtonShortPress("ScrollBPM") || input.GetNegativeButtonShortPress("ScrollBPM")){
            PartyFloor.instance.currentBPM += Mathf.Sign(dir * shortScroll);
            ShowUI();
        }
        else if (input.GetButtonDown("ScrollBPM") || input.GetNegativeButtonDown("ScrollBPM")){
            PartyFloor.instance.currentBPM += Mathf.Sign(dir);
            ShowUI();
        }
        PartyFloor.instance.currentBPM = Mathf.Max(PartyFloor.instance.currentBPM, 0);

        if (input.GetButtonDown("ToggleMute")){
            PlayMusic.ToggleAudio();
            ShowUI();
        }

        BPMText.text = "BPM: " + PartyFloor.instance.currentBPM;

        UpdateShowUI();
    }

    void ShowUI(){
        timePressed = Time.unscaledTime;
    }

    void UpdateShowUI(){
        if (textTransform){
            if (Time.unscaledTime - timePressed < showUITIme){
                textTransform.anchoredPosition = Vector3.Lerp(textTransform.anchoredPosition, textVisiblePos, 6*Time.unscaledDeltaTime);
            }
            else{
                textTransform.anchoredPosition = Vector3.Lerp(textTransform.anchoredPosition, textHiddenPos, 6*Time.unscaledDeltaTime);
            }
        }
    }
}
