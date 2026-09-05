using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrigerScript : MonoBehaviour
{
   


    public TriggersHolder holder;


    public Collider[] colliders;

    public LayerMask layer;


    private void Awake()
    {
        Rules.TriggerScriptEvent.AddListener(Check);
    }

    private void Start()
    {
        Check();
    }

    private void Update()
    {
        Check();
    }



    public void Check()
    {
        colliders = Physics.OverlapBox(transform.position, new Vector3(0.3f, 0.3f, 0.3f),Quaternion.identity,layer);
        

        if(colliders.Length > 0)
        {
            holder.TrigerEnter(gameObject);
        }
        

        
    }
}
