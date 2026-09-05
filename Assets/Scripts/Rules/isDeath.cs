using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class isDeath : Rule
{

    public static UnityEvent DeathEvent = new UnityEvent();

    public LayerMask layer;


    public bool death;


    private void Awake()
    {

        Rules.DeathEvent.AddListener(CheckDeath);

        Rules.RuleDiactivateEvent.AddListener(DisActivate);

        
    }

    public override void Activate()
    {
        base.Activate();

        death = true;

       
    }

    public override void DisActivate()
    {
        base.DisActivate();

        death = false;
    }



    public void CheckDeath()
    {
        if (death)
        {
            Collider[] colliders;

            colliders = Physics.OverlapBox(transform.position, new Vector3(0.3f, 0.3f, 0.3f), Quaternion.identity, layer);


            if (colliders.Length > 0)
            {


                foreach (Collider collider in colliders)
                {
                    if (collider.transform.parent.GetComponent<isYou>().you)
                    {

                        Death(collider);
                    }
                }
            }
        }
    }


    public void Death(Collider collider)
    {
        collider.enabled = false;
        Destroy(collider.transform.parent.parent.gameObject);

        DeathEvent.Invoke();

       
    }
}
