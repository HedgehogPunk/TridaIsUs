using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class isFly : Rule
{
    public bool fly;

    public override void Activate()
    {
        base.Activate();
        fly = true;
    }

    public override void DisActivate()
    {
        base.DisActivate();
        fly = false;
    }
}
