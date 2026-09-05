using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearRulesList : MonoBehaviour
{

    public Rules rules;

    private void Awake()
    {
        Rules.RuleDiactivateEvent.AddListener(ClearLists);
    }

    public void ClearLists()
    {

        
        Rules.StaticRules = rules.rules;


        rules.rules.Clear();
        rules.YouRules.Clear();
        rules.objToObjRules.Clear();
        rules.Actionrules.Clear();
        rules.WinRules.Clear();
        rules.DeathRules.Clear();

        rules.NotActionRules.Clear();
        rules.SelfEliminationRules.Clear();
        rules.NotObjectIsActionRules.Clear();
        rules.NotObjectIsNotActionRules.Clear();
    }
}
