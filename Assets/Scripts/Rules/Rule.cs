using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rule : MonoBehaviour
{

    public bool not;

    private void Awake()
    {
        Rules.RuleDiactivateEvent.AddListener(DisActivate);
    }

    public virtual void Activate()
    {

    }

    public virtual void DisActivate()
    {
        not = false;
    }
}
