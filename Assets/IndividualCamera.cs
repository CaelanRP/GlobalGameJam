﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndividualCamera : MonoBehaviour
{
    public Camera cam;
    public Camera individualCam;
    public Camera snapCam;
    public Camera topDownCam;

    public GameObject snapchat;
    public TMPro.TextMeshProUGUI text;
    public string[] dialogue;

    public AnimationCurve slowMoCurve;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FindIndividuals());
        cam.enabled = true;
    }

    // Update is called once per frame
    IEnumerator FindIndividuals()
    {
        yield return new WaitForSeconds(12 * PartyFloor.normalizedBPM);
        float defaultFixedDeltaTime = Time.fixedDeltaTime;
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(1,5) * 2 * PartyFloor.normalizedBPM);
            float rand = Random.value;
            if (rand < 0.33f)
            {
                if (PartyFloor.partygoers.Count != 0)
                {
                    Transform snapper = PartyFloor.partygoers[Random.Range(0, PartyFloor.partygoers.Count)].lookSpot;
                    Transform target = PartyFloor.partygoers[Random.Range(0, PartyFloor.partygoers.Count)].lookSpot;
                    if (snapper != null)
                    {
                        snapCam.transform.position = snapper.position;
                        snapper.rotation = target.rotation;
                        cam.enabled = false;
                        snapCam.gameObject.SetActive(true);
                        snapchat.SetActive(true);
                        float t = 0;
                        snapCam.transform.LookAt(new Vector3(0, 1, 0));
                        while (t < 1)
                        {
                            t += (Time.unscaledDeltaTime * PartyFloor.normalizedBPM) / 4;
                            //Time.timeScale = slowMoCurve.Evaluate(t);
                            //Time.fixedDeltaTime = defaultFixedDeltaTime * slowMoCurve.Evaluate(t);
                            snapCam.transform.LookAt(target);
                            yield return null;
                        }
                        //Time.timeScale = 1f;
                        //Time.fixedDeltaTime = defaultFixedDeltaTime;
                        cam.enabled = true;
                        snapCam.gameObject.SetActive(false);
                        snapchat.SetActive(false);
                    }
                }
            }
            else if (rand < 0.67f)
            {
                if (PartyFloor.partygoers.Count != 0) { 
                    Transform target = PartyFloor.partygoers[Random.Range(0, PartyFloor.partygoers.Count)].lookSpot;
                    if (target != null)
                    {
                        individualCam.transform.position = target.position;
                        cam.enabled = false;
                        individualCam.gameObject.SetActive(true);
                        float t = 0;
                        individualCam.transform.LookAt(new Vector3(0, 1, 0));
                        while (t < 1)
                        {
                            t += (Time.unscaledDeltaTime * PartyFloor.normalizedBPM) / 2;
                            Time.timeScale = slowMoCurve.Evaluate(t);
                            Time.fixedDeltaTime = defaultFixedDeltaTime * slowMoCurve.Evaluate(t);
                            //transform.Translate(transform.right * Time.deltaTime);
                            individualCam.transform.Rotate(Time.deltaTime, 0, 0);
                            //transform.LookAt(target);
                            yield return null;
                        }
                        Time.timeScale = 1f;
                        Time.fixedDeltaTime = defaultFixedDeltaTime;
                        cam.enabled = true;
                        individualCam.gameObject.SetActive(false);
                    }
                }
            }
            else
            {
                cam.enabled = false;
                Vector3 initPos = topDownCam.transform.position;
                topDownCam.gameObject.SetActive(true);
                float t = 0;
                while (t < 1)
                {
                    t += (Time.unscaledDeltaTime * PartyFloor.normalizedBPM) / 4;
                    topDownCam.transform.Translate(Vector3.forward * Time.deltaTime * 2);
                    yield return null;
                }

                cam.enabled = true;
                topDownCam.transform.position = initPos;
                topDownCam.gameObject.SetActive(false);
            }
        }
    }
}
