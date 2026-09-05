using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class isMove : Rule
{
    public bool move;


    private void Awake()
    {
        Rules.RuleDiactivateEvent.AddListener(DisActivate);

        Rules.DeathEvent.AddListener(Move);
    }

    public override void Activate()
    {
        base.Activate();

        move = true;

    }


    public override void DisActivate()
    {
        base.DisActivate();

        move = false;
    }


    public void Move()
    {
        if(move)
        {
            GameBlock GB = transform.parent.GetComponent<GameBlock>();

            GetComponent<isYou>().moving = true;

            GetComponent<isYou>().Move(GB.VectorToDir(GB.transform.forward), GB.transform.forward);
        }
    }
}
