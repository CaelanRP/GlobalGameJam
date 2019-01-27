using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using NaughtyAttributes;

public class PartyProp : MonoBehaviour
{
    protected Animator animator;
    public const int LAYER = 12;
    private const string Name = "speed";
    private Rigidbody _rb;
    private Rigidbody rb{
        get{
            if (!_rb){
                _rb = GetComponent<Rigidbody>();
            }
            return _rb;
        }
    }
    public Vector3 minSpawnPos, maxSpawnPos;

    protected virtual void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (animator)
        animator.SetFloat(Name, PartyFloor.normalizedBPM);
    }

    public PartyProp Spawn(){
        PartyProp prop = Instantiate(gameObject, GetSpawnVector(), transform.rotation).GetComponent<PartyProp>();
        if (prop.rb){
            RaycastHit[] hits = prop.rb.SweepTestAll(Vector3.up, 0.1f);
            int tries = 4;
        
            while(hits.Any(h => h.collider.GetComponent<PartyProp>())){
                prop.transform.position = GetSpawnVector();
                hits = prop.rb.SweepTestAll(Vector3.up, 0.1f);
                tries --;
                if (tries == 0){
                    Destroy(prop.gameObject);
                    return null;
                }
            }
        }
        return prop;
    }

    void Start(){
        PartyFloor.props.Add(this);
    }

    public Vector3 GetSpawnVector(){
        float x = UnityEngine.Random.Range(minSpawnPos.x, maxSpawnPos.x);
        float y = UnityEngine.Random.Range(minSpawnPos.y, maxSpawnPos.y);
        float z = UnityEngine.Random.Range(minSpawnPos.z, maxSpawnPos.z);
        return new Vector3(x,y,z);
    }
}
