using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class isRotate : Rule
{

    public bool rot;

    public bool canRot;
    private void Awake()
    {
        Rules.DeathEvent.AddListener(Rotate);

        Rules.RuleDiactivateEvent.AddListener(DisActivate);
    }

    public override void DisActivate()
    {
        base.DisActivate();

        rot = false;

        canRot = false;
    }

    public override void Activate()
    {
        base.Activate();

        canRot = true;

        
    }


    public void Rotate()
    {
        if (canRot)
        {
            if (!not && !rot)
            {
                transform.parent.eulerAngles = new Vector3(transform.parent.rotation.x, transform.rotation.eulerAngles.y + 90f, transform.rotation.eulerAngles.z);

                rot = true;
            }
        }
    }
}
