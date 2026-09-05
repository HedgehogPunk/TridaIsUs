using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class isPush : Rule
{
    public BoxCollider boxCollider;

    public bool push;


   

    public override void Activate()
    {
        base.Activate();

        push = true;
        boxCollider.enabled = true;
    }


    public override void DisActivate()
    {
        base.DisActivate();

        push = false;
        boxCollider.enabled = false;
    }

    
}
