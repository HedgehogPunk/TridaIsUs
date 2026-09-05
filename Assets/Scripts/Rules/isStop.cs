using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class isStop : Rule
{
    public BoxCollider boxCollider;





    public override void Activate()
    {
        base.Activate();

        boxCollider.enabled = true;
    }

    public override void DisActivate()
    {
        base.DisActivate();

        

        if (GetComponent<isPush>().push)
        {
            return;
        }
        else
        {
           
            boxCollider.enabled = false;
        }
    }

}
