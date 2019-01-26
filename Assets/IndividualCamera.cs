using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndividualCamera : MonoBehaviour
{
    Camera cam;
    public AnimationCurve slowMoCurve;
    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
        StartCoroutine(FindIndividuals());
        cam.enabled = false;
    }

    // Update is called once per frame
    IEnumerator FindIndividuals()
    {
        float defaultFixedDeltaTime = Time.fixedDeltaTime;
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(1,5));
            PartyFloor pf = GameObject.FindObjectOfType<PartyFloor>();
            Transform target = pf.partygoers[Random.Range(0, pf.partygoers.Count)].lookSpot;
            transform.position = target.position;
            target.rotation = target.rotation;
            cam.enabled = true;
            float t = 0;
            transform.LookAt(new Vector3(0,1,0));
            while (t < 1)
            {
                t += Time.unscaledDeltaTime/2;
                Time.timeScale =  slowMoCurve.Evaluate(t);
                Time.fixedDeltaTime = defaultFixedDeltaTime * slowMoCurve.Evaluate(t);
                //transform.Translate(transform.right * Time.deltaTime);
                transform.Rotate(Time.deltaTime, 0, 0);
                //transform.LookAt(target);
                yield return null;
            }
            Time.timeScale = 1f;
            Time.fixedDeltaTime = defaultFixedDeltaTime;
            cam.enabled = false;
        }
    }
}
