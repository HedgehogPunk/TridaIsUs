using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class isShut : Rule
{
    public bool shut;

    public LayerMask layer;


    private void Awake()
    {

        Rules.DeathEvent.AddListener(CheckDeath);

        Rules.RuleDiactivateEvent.AddListener(DisActivate);


    }


    public override void Activate()
    {
        base.Activate();

        shut = true;

    }


    public override void DisActivate()
    {
        base.DisActivate();

        shut = false;
    }

    public void CheckDeath()
    {
        if (shut)
        {
            Collider[] colliders;

            colliders = Physics.OverlapBox(transform.position, new Vector3(0.3f, 0.3f, 0.3f), Quaternion.identity, layer);


            bool kill = false;

            if (colliders.Length > 0)
            {


                foreach (Collider collider in colliders)
                {
                    if (collider.transform.parent.GetComponent<isOpen>().open)
                    {
                        kill = true;
                        Death(collider);
                    }
                }

                if(kill)
                {
                    Death(GetComponent<Code>().PlayerLayerCollider);

                    // звук
                }
            }
        }
    }


    public void Death(Collider collider)
    {
        collider.enabled = false;
        Destroy(collider.transform.parent.parent.gameObject);
    }
}
