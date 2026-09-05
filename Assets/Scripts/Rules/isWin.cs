using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class isWin : Rule
{

    public static UnityEvent WinEvent = new UnityEvent();

    public ParticleSystem winPart;

    public LayerMask layer;

    public bool win;

    private void Awake()
    {

        Rules.RuleDiactivateEvent.AddListener(DisActivate);

        Rules.CanMoveEvent.AddListener(TurnPart);
    }

    public void TurnPart()
    {
        if(win)
        {
            winPart.Play();
        }
        else
        {
            winPart.Stop();
        }
    }
    public override void Activate()
    {
        base.Activate();

        win = true;

        winPart.Play();

        Collider[] colliders;

        colliders = Physics.OverlapBox(transform.position, new Vector3(0.3f, 0.3f, 0.3f), Quaternion.identity, layer);


        if (colliders.Length > 0)
        {
            foreach(Collider collider in colliders)
            {
                if(collider.transform.parent.GetComponent<isYou>().you)
                {
                    Win();
                }
            }
        }
    }

    public override void DisActivate()
    {
        base.DisActivate();

        win = false;
    }


    public void Win()
    {
       
        WinEvent.Invoke();
    }
}
