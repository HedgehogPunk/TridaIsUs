using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
public class Rules : MonoBehaviour
{

    public static UnityEvent nextTurnEvent = new UnityEvent();
    public static UnityEvent RuleDiactivateEvent = new UnityEvent();
    public static UnityEvent TextBlockEvent = new UnityEvent();
    public static UnityEvent TriggerScriptEvent = new UnityEvent();
    public static UnityEvent TriggerHolderEvent = new UnityEvent();

    public static UnityEvent AddTurnEvent = new UnityEvent();

    public static UnityEvent CanMoveEvent = new UnityEvent();

    public static UnityEvent PauseEvent = new UnityEvent();

    public static UnityEvent DeathEvent = new UnityEvent();

    public YouManager youManager;

    public List<string> sushWords; // wall door key trida
    public List<string> operators; // is and
    public List<string> actions; // stop push open win you


    public Dictionary<string, GameObject> prefabDic = new Dictionary<string, GameObject>();
    public List<string> DicStrings;
    public List<GameObject> DicPrefabs;

    public Dictionary<string, GameObject> UndoPrefabDic = new Dictionary<string, GameObject>();
    public List<string> UndoDicStrings;
    public List<GameObject> UndoDicPrefabs;



    public List<List<string>> rules = new List<List<string>>();
    public List<List<string>> objToObjRules = new List<List<string>>();
    public List<List<string>> Actionrules = new List<List<string>>();
    public List<List<string>> YouRules = new List<List<string>>();
    public List<List<string>> WinRules = new List<List<string>>();
    public List<List<string>> DeathRules = new List<List<string>>();


    public List<List<string>> NotActionRules = new List<List<string>>();
    public List<List<string>> SelfEliminationRules = new List<List<string>>();
    public List<List<string>> NotObjectIsNotActionRules = new List<List<string>>();
    public List<List<string>> NotObjectIsActionRules = new List<List<string>>();

    public static List<List<string>> StaticRules = new List<List<string>>();


    InputMaster inputs;

    public List<Turn> Turns = new List<Turn>();

    bool undo = false;

    private void Awake()
    {
        FillDic();

        nextTurnEvent.AddListener(CheckRulesTurn);

        inputs = new InputMaster();
        inputs.Enable();
        //inputs.Movement.WaitButton.performed += c => Wait();

        AddTurnEvent.AddListener(AddTurn);
    }
    public void FillDic()
    {
        for(int i = 0; i < DicStrings.Count; i++)
        {
            prefabDic.Add(DicStrings[i], DicPrefabs[i]);
        }

        for (int i = 0; i < UndoDicStrings.Count; i++)
        {
            UndoPrefabDic.Add(UndoDicStrings[i], UndoDicPrefabs[i]);
        }

    }

    private void Start()
    {
        CheckRulesTurn();

        
    }



    public void Undo()
    {
        Debug.Log(Turns.Count);

        if (Turns.Count > 0)
        {
            DestroyAll();

            foreach (SaveObject so in Turns[Turns.Count - 1].sos)
            {
                Instantiate(UndoPrefabDic[so.prefab], so.position, so.rotation, transform);
            }

            /*
            rules = Turns[Turns.Count - 1].rules;

            CheckRules();
            */

            Turns.Remove(Turns[Turns.Count - 1]);

            undo = true;

            youManager.NextTurn();


        }
        
    }
    public void DestroyAll()
    {
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            
            Destroy(transform.GetChild(i).gameObject);
        }
    }
    public void AddTurn()
    {
        Turn turn = new Turn();

        turn.rules = rules;

        List<SaveObject> turnsos = new List<SaveObject>();

        for (int i = 0; i < transform.childCount; i++)
        {
            SaveObject so = new SaveObject();

            so.prefab = transform.GetChild(i).GetComponent<GameBlock>().prefab;

            so.position = transform.GetChild(i).position;

            so.rotation = transform.GetChild(i).rotation;

            turnsos.Add(so);
        }
        turn.sos = turnsos;

        Turns.Add(turn);

        undo = false;
    }


    public void CheckRulesTurn()
    {
        rules.AddRange(SelfEliminationRules);

        rules.AddRange(NotObjectIsNotActionRules);

        rules.AddRange(NotActionRules);

        rules.AddRange(objToObjRules);

        rules.AddRange(YouRules);

        rules.AddRange(DeathRules);

        rules.AddRange(Actionrules);

        rules.AddRange(WinRules);

        rules.AddRange(NotObjectIsActionRules);

        



        RemoveClones();

        StaticRules = rules;

        CheckRules();

       

    }

    public void CheckRules()
    {
       


        foreach (List<string> rule in rules)
        {
            if(rule.Count == 3)
            {
                ThreeWordRule(rule);
            }

            else
            {
                ManyWordRule(rule);
            }
        }
    }

    private void Update()
    {
        if(inputs.Movement.Restart.triggered)
        {
            Restart();
        }

        if(inputs.Movement.Pause.triggered)
        {

            RemoveClones();
            StaticRules = rules;

            PauseEvent.Invoke();
        }

        if(inputs.Movement.Undo.inProgress && ! youManager.waiting && youManager.canMove)
        {
            Undo();
        }

        if (inputs.Movement.WaitButton.triggered)
        {
            Wait();
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public static void Wait()
    {
        AddTurnEvent.Invoke();

        DeathEvent.Invoke();
        
        RuleDiactivateEvent.Invoke();
        TriggerHolderEvent.Invoke();
        TriggerScriptEvent.Invoke();

        TextBlockEvent.Invoke();
       

        nextTurnEvent.Invoke();

        CanMoveEvent.Invoke();
    }



    public void isThatARule(List<string> rule)
    {



        if (rule.Count == 3)
        {

            if (sushWords.Contains(rule[0]))
            {

                if (operators.Contains(rule[1]))
                {
                    if (rule[1] == "Is")
                    {




                        if (actions.Contains(rule[2]) && rule[2] == "You")
                        {
                            YouRules.Add(rule);
                            return;
                        }

                        if (actions.Contains(rule[2]) && rule[2] == "Win")
                        {
                            WinRules.Add(rule);
                            return;
                        }
                        if (actions.Contains(rule[2]) && rule[2] == "Death")
                        {
                            DeathRules.Add(rule);
                            return;
                        }


                        if (sushWords.Contains(rule[2]))
                        {
                            objToObjRules.Add(rule);
                        }


                        if (actions.Contains(rule[2]))
                        {
                            Actionrules.Add(rule);
                        }
                    }

                }


            }
        }
        else
        {
            MoreThanTri(rule);
        }
        
    }
    public void MoreThanTri(List<string> rule)
    {
        if (rule[0] == "Not")
        {
            if (sushWords.Contains(rule[1]))
            {
                LongRule(rule, 2);
            }
            else if (rule[1] == "Not")
            {
                DoubleNot(rule, 0);
            }


        }
        else if (sushWords.Contains(rule[0]))
        {
            LongRule(rule, 1);
        }
    }
    public void LongRule(List<string> rule, int StartIndex)
    {
        if (operators.Contains(rule[StartIndex]))
        {
            if (rule[StartIndex] == "Is")
            {
                if (rule[StartIndex+1] == "Not")
                {
                    if (rule.Count > StartIndex + 2)
                    {
                        if (actions.Contains(rule[StartIndex + 2]) && rule[0] != "Not")
                        {

                            if (rule.Count > StartIndex + 3)
                            {
                                AndAction(rule, StartIndex + 1);
                            }
                            else
                            {
                                NotActionRules.Add(rule);
                            }

                        }
                        else if (sushWords.Contains(rule[StartIndex + 2]) && rule[0] != "Not")
                        {
                            SelfEliminationRules.Add(rule);
                        }
                        else if (rule[StartIndex + 2] == "Not")
                        {
                            DoubleNot(rule, StartIndex + 1);
                        }
                        else if (rule[0] == "Not" && actions.Contains(rule[StartIndex + 2]))
                        {
                            if (rule.Count > StartIndex + 3)
                            {
                                AndAction(rule, StartIndex + 1);
                            }
                            else
                            {
                                NotObjectIsNotActionRules.Add(rule);
                            }

                        }


                    }
                }
                else if (rule[0] == "Not" && actions.Contains(rule[StartIndex + 1]))
                {

                    if(rule.Count > StartIndex + 2)
                    {
                        AndAction(rule, StartIndex);
                    }
                    else
                    {
                        NotObjectIsActionRules.Add(rule);
                    }

                   

                    AndAction(rule, StartIndex);

                      
                }
                else if (actions.Contains(rule[StartIndex + 1]))
                {
                    AndAction(rule, StartIndex);
                }
            }
            else if (rule[StartIndex] == "And")
            {

                AndObject(rule, StartIndex);
            }
        }
    }

    public void AndAction(List<string> rule, int StartIndex)
    {
       

        if (rule.Count > StartIndex + 2)
        {
            if (rule[StartIndex + 2] == "And")
            {

                


                if (rule.Count > StartIndex + 3)
                {
                    if (actions.Contains(rule[StartIndex + 3]))
                    {
                        if (rule[StartIndex] == "Not")
                        {
                            rule.RemoveRange(StartIndex, 3);

                            if (rule.Count >= 3)
                            {
                              


                                isThatARule(rule);
                            }
                        }
                        else
                        {
                            rule.RemoveRange(StartIndex + 1, 2);

                            if (rule.Count >= 3)
                            {
                               

                                isThatARule(rule);
                            }
                        }
                    }

                }

            }
        }
    }

    public void AndObject(List<string> rule, int StartIndex)
    {
        if (rule.Count > StartIndex + 1)
        {
            if (sushWords.Contains(rule[StartIndex + 1]))
            {
                rule.RemoveRange(StartIndex, 2);

                if (rule.Count >= 3)
                {
                    isThatARule(rule);
                }
            }
            else if (rule[StartIndex + 1] == "Not")
            {
                if (rule.Count > StartIndex + 2)
                {
                    if (sushWords.Contains(rule[StartIndex + 2]))
                    {
                        rule.RemoveRange(StartIndex, 3);

                        if (rule.Count >= 3)
                        {
                            isThatARule(rule);
                        }
                    }
                }
            }
        }
    }
    public void DoubleNot(List<string> rule, int i)
    {
        rule.RemoveRange(i, 2);

        if (rule.Count >= 3)
        {
            isThatARule(rule);
        }
    }


    public void ManyWordRule(List<string> rule)
    {
        if(ContainRule(NotObjectIsActionRules,rule))
        {
            NotObjectAction(rule[1], rule[3]);
        }
        else if (ContainRule(NotObjectIsNotActionRules, rule))
        {
            NotObjectNotAction(rule[1], rule[4]);
        }
        else if (ContainRule(SelfEliminationRules, rule))
        {
            Elimination(rule[0]);
        }
        else if(ContainRule(NotActionRules, rule))
        {
            NotAction(rule[0], rule[3]);
        }
    }

    public void NotAction(string obj, string action)
    {
        List<string> antiRule = new List<string>() {"Not",obj,"Is","Not",action };

        

        if(ContainRule(rules,antiRule))
        {
            Debug.Log("Yes");
            return;
        }

        for (int i = 0; i < transform.childCount; i++)
        {



            if (transform.GetChild(i).GetComponent<GameBlock>().obj == obj || obj == "All")
            {

                Code code = transform.GetChild(i).GetComponent<GameBlock>().code;


                code.ActionDic[action].not = true;

            }
        }
    }
    public  void NotObjectNotAction(string obj, string action)
    {
        for (int i = 0; i < transform.childCount; i++)
        {



            if (transform.GetChild(i).GetComponent<GameBlock>().obj != obj)
            {

                Code code = transform.GetChild(i).GetComponent<GameBlock>().code;


                code.ActionDic[action].not = true;

            }
        }
    }
    public void Elimination(string obj)
    {
        for (int i = 0; i < transform.childCount; i++)
        {



            if (transform.GetChild(i).GetComponent<GameBlock>().obj == obj || obj == "All")
            {

                Destroy(transform.GetChild(i).gameObject);

            }
        }
    }
    public void NotObjectAction(string obj, string action)
    {
       


        for (int i = 0; i < transform.childCount; i++)
        {

            if (transform.GetChild(i).GetComponent<GameBlock>().obj != obj)
            {

                Code code = transform.GetChild(i).GetComponent<GameBlock>().code;



                if (code.ActionDic[action].not)
                {
                   continue;
                }

                code.ActionDic[action].Activate();




                if (action == "You" && !youManager.yous.Contains(transform.GetChild(i).GetComponent<GameBlock>().code))
                {
                    youManager.yous.Add(transform.GetChild(i).GetComponent<GameBlock>().code);
                }

            }
        }
    }


    public void ThreeWordRule(List<string> rule)
    {
        if (sushWords.Contains(rule[2]) && undo == false)
        {
            ObjToObj(rule[0], rule[2]);
        }

        if (actions.Contains(rule[2]))
        {
            Actions(rule[0],rule[1], rule[2]);
        }

    }

    public void ObjToObj(string from, string to)
    {
        
        if(from == to)
        {
            Debug.Log("Equals");
            return;
        }


        List<string> checkList = new List<string>();

        checkList.Add(from);
        checkList.Add("Is");
        checkList.Add(from);


        for (int i = 0; i < transform.childCount; i++)
        {

          

           if((transform.GetChild(i).GetComponent<GameBlock>().obj == from || from == "All")
                && ! transform.GetChild(i).GetComponent<GameBlock>().spawnedOnThisTurn  
                && ! ContainRule(rules,checkList))
            {

                if(to == "All")
                {
                    return;
                }

                if(to == "Text")
                {
                   

                    GameBlock block = transform.GetChild(i).transform.GetComponent<GameBlock>();

                    GameObject newTextObj = Instantiate(block.code.TextPrefab, transform.GetChild(i).transform.position, Quaternion.identity, transform);
                    newTextObj.GetComponent<GameBlock>().spawnedOnThisTurn = true;
                   

                    Destroy(transform.GetChild(i).gameObject);

                    
                }
                else
                {
                    GameObject newObj = Instantiate(prefabDic[to], transform.GetChild(i).transform.position, Quaternion.identity, transform);
                    newObj.GetComponent<GameBlock>().spawnedOnThisTurn = true;
                    

                    Destroy(transform.GetChild(i).gameObject);




                }
            }
        }
    }

    public void Actions(string obj,string oper, string action)
    {
        List<string> notRule = new List<string>() { "Not", obj, oper, action };
        


        if (ContainRule(rules,notRule))
        {
            return;
        }

        for (int i = 0; i < transform.childCount; i++)
        {



            if (transform.GetChild(i).GetComponent<GameBlock>().obj == obj || obj == "All")
            {

                Code code = transform.GetChild(i).GetComponent<GameBlock>().code;


                if (code.ActionDic[action].not)
                {
                    continue;
                }


                code.ActionDic[action].Activate();

                


                if (action == "You" && ! youManager.yous.Contains(transform.GetChild(i).GetComponent<GameBlock>().code))
                {
                    youManager.yous.Add(transform.GetChild(i).GetComponent<GameBlock>().code);
                }
            }
        }
    }


    public bool ContainRule(List<List<string>> ruleList, List<string> rule)
    {
        foreach(List<string> r in ruleList)
        {
           


            if (r.Count == rule.Count)
            {
               

                bool eq = true;

                for (int i = 0; i < rule.Count; i++)
                {


                    if (r[i] != rule[i])
                    {
                        eq = false;
                    }

                    
                }

               


                if(eq)
                {
                    return eq;
                }
               
            }

           

           
        }

        return false;
    }

    public void RemoveClones()
    {
        for(int i = 0; i < rules.Count; i++)
        {
            for(int j = 0; j < rules.Count; j++)
            {

                if (rules[i].Count == rules[j].Count && rules[i] != rules[j])
                {



                    bool eq = true;

                    for (int x = 0; x < rules[i].Count; x++)
                    {


                        if (rules[i][x] != rules[j][x])
                        {
                            eq = false;
                        }


                    }

                    if (eq)
                    {
                        rules.RemoveAt(j);

                        return;
                    }

                    List<string> test = new List<string>();

                    test.Add("Not");
                    test.AddRange(rules[j]);

                    if(ContainRule(rules,test))
                    {
                        rules.RemoveAt(j);
                    }
                }
            }
        }
    }

    /*
     *  if(rules[i] != rules[j] && rules[i][0] == rules[j][0] && rules[i][1] == rules[j][1] && rules[i][2] == rules[j][2])
                {
                    rules.RemoveAt(j);
                }
     * 
     * 
     */


}

public class SaveObject
{
    public string prefab;

    public Vector3 position;

    public Quaternion rotation;
}

public class Turn
{
    public List<SaveObject> sos;

    public List<List<string>> rules;
}
