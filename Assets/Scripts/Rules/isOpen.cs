using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class isOpen : Rule
{
    public bool open;


    private void Awake()
    {
        Rules.RuleDiactivateEvent.AddListener(DisActivate);
    }

    public override void Activate()
    {
        base.Activate();

        open = true;
        
    }


    public override void DisActivate()
    {
        base.DisActivate();

        open = false;
    }
}
