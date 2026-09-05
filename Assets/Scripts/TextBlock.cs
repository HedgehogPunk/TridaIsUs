using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextBlock : MonoBehaviour
{
    public string word;

    public bool multiWord = false;

   

  


    public Code code;

    public Transform WordPanelsParent;


    private void Awake()
    {

        Rules.TextBlockEvent.AddListener(ClearRule);
    }

    private void Start()
    {
        ClearRule();

    }
    

    public void ClearRule()
    {



        /*
         CheckRules(Direction.right);
         

         CheckRules(Direction.down);
         
    

        CheckRules(Direction.back);
        */
        
        CheckRules(Direction.left);


        CheckRules(Direction.up);



        CheckRules(Direction.forward);
        




    }


    public void CheckRulesEdge(Direction dir, List<string> r, Vector3 vector)
    {
        List<string> rule = new List<string>();

        for (int i = 0; i < WordPanelsParent.childCount; i++)
        {
            if (WordPanelsParent.GetChild(i).transform.up == vector)
            {
                rule.Add(WordPanelsParent.GetChild(i).GetComponent<EdgeOfText>().words[Language.English]);
                rule.AddRange(r);

                break;
            }
        }

        if(rule.Count >= 3)
        {
            transform.parent.GetComponent<Rules>().isThatARule(rule);

            if (rule.Count >= 25)
            {
                return;
            }
        }

        if (code.thText.directions[dir])
        {

            TextBlock otherText;


            if (code.thText.triggersGameObjects[dir].GetComponent<TrigerScript>().colliders.Length > 0)
            {

                foreach (Collider collider in code.thText.triggersGameObjects[dir].GetComponent<TrigerScript>().colliders)
                {
                    if (collider.GetComponent<TextBlock>())
                    {
                        otherText = collider.GetComponent<TextBlock>();

                        if (otherText != this)
                        {
                            otherText.CheckRulesEdge(dir, rule,vector);
                        }
                    }
                }
            }

        }


    }

    public void CheckRulesList(Direction dir, List<string> r)
    {
        List<string> ruleRight = new List<string>();
        List<string> ruleDown = new List<string>();
        List<string> ruleBack = new List<string>();
        List<string> ruleLeft = new List<string>();
        List<string> ruleUp = new List<string>();
        List<string> ruleForward = new List<string>();

        List<string> rule = new List<string>();

        if (multiWord)
        {


            if (dir == Direction.forward)
            {
                for (int i = 0; i < WordPanelsParent.childCount; i++)
                {
                    if (WordPanelsParent.GetChild(i).transform.up == Vector3.right)
                    {
                        ruleRight.Add(WordPanelsParent.GetChild(i).GetComponent<EdgeOfText>().words[Language.English]);
                        ruleRight.AddRange(r);
                    }
                    if (WordPanelsParent.GetChild(i).transform.up == Vector3.left)
                    {
                        ruleLeft.Add(WordPanelsParent.GetChild(i).GetComponent<EdgeOfText>().words[Language.English]);
                        ruleLeft.AddRange(r);
                    }
                    if (WordPanelsParent.GetChild(i).transform.up == Vector3.up)
                    {
                        ruleUp.Add(WordPanelsParent.GetChild(i).GetComponent<EdgeOfText>().words[Language.English]);
                        ruleUp.AddRange(r);
                    }
                    if (WordPanelsParent.GetChild(i).transform.up == Vector3.down)
                    {
                        ruleDown.Add(WordPanelsParent.GetChild(i).GetComponent<EdgeOfText>().words[Language.English]);
                        ruleDown.AddRange(r);
                    }
                }
            }

            if (dir == Direction.up)
            {
                for (int i = 0; i < WordPanelsParent.childCount; i++)
                {
                    if (WordPanelsParent.GetChild(i).transform.up == Vector3.right)
                    {
                        ruleRight.Add(WordPanelsParent.GetChild(i).GetComponent<EdgeOfText>().words[Language.English]);
                        ruleRight.AddRange(r);
                    }
                    if (WordPanelsParent.GetChild(i).transform.up == Vector3.left)
                    {
                        ruleLeft.Add(WordPanelsParent.GetChild(i).GetComponent<EdgeOfText>().words[Language.English]);
                        ruleLeft.AddRange(r);
                    }
                    if (WordPanelsParent.GetChild(i).transform.up == Vector3.forward)
                    {
                        ruleForward.Add(WordPanelsParent.GetChild(i).GetComponent<EdgeOfText>().words[Language.English]);
                        ruleForward.AddRange(r);
                    }
                    if (WordPanelsParent.GetChild(i).transform.up == Vector3.back)
                    {
                        ruleBack.Add(WordPanelsParent.GetChild(i).GetComponent<EdgeOfText>().words[Language.English]);
                        ruleBack.AddRange(r);
                    }
                }
            }

            if (dir == Direction.left)
            {
                for (int i = 0; i < WordPanelsParent.childCount; i++)
                {
                    if (WordPanelsParent.GetChild(i).transform.up == Vector3.forward)
                    {
                        ruleForward.Add(WordPanelsParent.GetChild(i).GetComponent<EdgeOfText>().words[Language.English]);
                        ruleForward.AddRange(r);
                    }
                    if (WordPanelsParent.GetChild(i).transform.up == Vector3.back)
                    {
                        ruleBack.Add(WordPanelsParent.GetChild(i).GetComponent<EdgeOfText>().words[Language.English]);
                        ruleBack.AddRange(r);
                    }
                    if (WordPanelsParent.GetChild(i).transform.up == Vector3.up)
                    {
                        ruleUp.Add(WordPanelsParent.GetChild(i).GetComponent<EdgeOfText>().words[Language.English]);
                        ruleUp.AddRange(r);
                    }
                    if (WordPanelsParent.GetChild(i).transform.up == Vector3.down)
                    {
                        ruleDown.Add(WordPanelsParent.GetChild(i).GetComponent<EdgeOfText>().words[Language.English]);
                        ruleDown.AddRange(r);
                    }
                }
            }


            if (ruleRight.Count >= 3)
            {
                transform.parent.GetComponent<Rules>().isThatARule(ruleRight);
            }
            if (ruleLeft.Count >= 3)
            {
                transform.parent.GetComponent<Rules>().isThatARule(ruleLeft);
            }
            if (ruleUp.Count >= 3)
            {
                transform.parent.GetComponent<Rules>().isThatARule(ruleUp);
            }
            if (ruleDown.Count >= 3)
            {
                transform.parent.GetComponent<Rules>().isThatARule(ruleDown);
            }
            if (ruleForward.Count >= 3)
            {
                transform.parent.GetComponent<Rules>().isThatARule(ruleForward);
            }
            if (ruleBack.Count >= 3)
            {
                transform.parent.GetComponent<Rules>().isThatARule(ruleBack);
            }


        }
        else
        {

            rule.Add(word);
            rule.AddRange(r);


            if (rule.Count >= 3)
            {
                transform.parent.GetComponent<Rules>().isThatARule(rule);

                if (rule.Count >= 25)
                {
                    return;
                }
            }

        }

        if (code.thText.directions[dir])
        {

            TextBlock otherText;


            if(code.thText.triggersGameObjects[dir].GetComponent<TrigerScript>().colliders.Length > 0)
            {

                foreach (Collider collider in code.thText.triggersGameObjects[dir].GetComponent<TrigerScript>().colliders)
                {
                    if(collider.GetComponent<TextBlock>())
                    {
                        otherText = collider.GetComponent<TextBlock>();

                        if(otherText != this)
                        {

                            if(multiWord)
                            {
                                if(dir == Direction.forward)
                                {
                                    otherText.CheckRulesEdge(dir, ruleRight, Vector3.right);
                                    otherText.CheckRulesEdge(dir, ruleLeft, Vector3.left);;
                                    otherText.CheckRulesEdge(dir, ruleUp, Vector3.up);
                                    otherText.CheckRulesEdge(dir, ruleDown, Vector3.down);
                                }

                                if (dir == Direction.left)
                                {
                                    otherText.CheckRulesEdge(dir, ruleForward, Vector3.forward);
                                    otherText.CheckRulesEdge(dir, ruleBack, Vector3.back);
                                    otherText.CheckRulesEdge(dir, ruleUp, Vector3.up);
                                    otherText.CheckRulesEdge(dir, ruleDown, Vector3.down);
                                }

                                if (dir == Direction.up)
                                {
                                    otherText.CheckRulesEdge(dir, ruleRight, Vector3.right);
                                    otherText.CheckRulesEdge(dir, ruleLeft, Vector3.left); ;
                                    otherText.CheckRulesEdge(dir, ruleForward, Vector3.forward);
                                    otherText.CheckRulesEdge(dir, ruleBack, Vector3.back);
                                }

                            }
                            else
                            {
                                otherText.CheckRulesList(dir, rule);
                            }
                        }
                    }
                }
            }

        }

       
    }

    public void CheckRules(Direction dir)
    {
        List<string> ruleRight = new List<string>();
        List<string> ruleDown = new List<string>();
        List<string> ruleBack = new List<string>();
        List<string> ruleLeft = new List<string>();
        List<string> ruleUp = new List<string>();
        List<string> ruleForward = new List<string>();

        List<string> rule = new List<string>();

        if (multiWord)
        {


            if (dir == Direction.forward)
            {
                for (int i = 0; i < WordPanelsParent.childCount; i++)
                {
                    if (WordPanelsParent.GetChild(i).transform.up == Vector3.right)
                    {
                        ruleRight.Add(WordPanelsParent.GetChild(i).GetComponent<EdgeOfText>().words[Language.English]);
                  
                    }
                    if (WordPanelsParent.GetChild(i).transform.up == Vector3.left)
                    {
                        ruleLeft.Add(WordPanelsParent.GetChild(i).GetComponent<EdgeOfText>().words[Language.English]);
                  
                    }
                    if (WordPanelsParent.GetChild(i).transform.up == Vector3.up)
                    {
                        ruleUp.Add(WordPanelsParent.GetChild(i).GetComponent<EdgeOfText>().words[Language.English]);
                
                    }
                    if (WordPanelsParent.GetChild(i).transform.up == Vector3.down)
                    {
                        ruleDown.Add(WordPanelsParent.GetChild(i).GetComponent<EdgeOfText>().words[Language.English]);
               
                    }
                }
            }

            if (dir == Direction.up)
            {
                for (int i = 0; i < WordPanelsParent.childCount; i++)
                {
                    if (WordPanelsParent.GetChild(i).transform.up == Vector3.right)
                    {
                        ruleRight.Add(WordPanelsParent.GetChild(i).GetComponent<EdgeOfText>().words[Language.English]);
                     
                    }
                    if (WordPanelsParent.GetChild(i).transform.up == Vector3.left)
                    {
                        ruleLeft.Add(WordPanelsParent.GetChild(i).GetComponent<EdgeOfText>().words[Language.English]);
                     
                    }
                    if (WordPanelsParent.GetChild(i).transform.up == Vector3.forward)
                    {
                        ruleForward.Add(WordPanelsParent.GetChild(i).GetComponent<EdgeOfText>().words[Language.English]);

                    }
                    if (WordPanelsParent.GetChild(i).transform.up == Vector3.back)
                    {
                        ruleBack.Add(WordPanelsParent.GetChild(i).GetComponent<EdgeOfText>().words[Language.English]);
               
                    }
                }
            }

            if (dir == Direction.left)
            {
                for (int i = 0; i < WordPanelsParent.childCount; i++)
                {
                    if (WordPanelsParent.GetChild(i).transform.up == Vector3.forward)
                    {
                        ruleForward.Add(WordPanelsParent.GetChild(i).GetComponent<EdgeOfText>().words[Language.English]);
                      
                    }
                    if (WordPanelsParent.GetChild(i).transform.up == Vector3.back)
                    {
                        ruleBack.Add(WordPanelsParent.GetChild(i).GetComponent<EdgeOfText>().words[Language.English]);
                    }
                    if (WordPanelsParent.GetChild(i).transform.up == Vector3.up)
                
                    {
                        ruleUp.Add(WordPanelsParent.GetChild(i).GetComponent<EdgeOfText>().words[Language.English]);
                  
                    }
                    if (WordPanelsParent.GetChild(i).transform.up == Vector3.down)
                    {
                        ruleDown.Add(WordPanelsParent.GetChild(i).GetComponent<EdgeOfText>().words[Language.English]);
                      
                    }
                }
            }


           

        }


        rule.Add(word);

       


        if (code.thText.directions[dir])
        {

            TextBlock otherText;


            if (code.thText.triggersGameObjects[dir].GetComponent<TrigerScript>().colliders.Length > 0)
            {

                foreach (Collider collider in code.thText.triggersGameObjects[dir].GetComponent<TrigerScript>().colliders)
                {
                    if (collider.GetComponent<TextBlock>())
                    {
                        otherText = collider.GetComponent<TextBlock>();

                        if (otherText != this)
                        {
                            if (multiWord)
                            {
                                if (dir == Direction.forward)
                                {
                                    otherText.CheckRulesEdge(dir, ruleRight, Vector3.right);
                                    otherText.CheckRulesEdge(dir, ruleLeft, Vector3.left); ;
                                    otherText.CheckRulesEdge(dir, ruleUp, Vector3.up);
                                    otherText.CheckRulesEdge(dir, ruleDown, Vector3.down);
                                }

                                if (dir == Direction.left)
                                {
                                    otherText.CheckRulesEdge(dir, ruleForward, Vector3.forward);
                                    otherText.CheckRulesEdge(dir, ruleBack, Vector3.back);
                                    otherText.CheckRulesEdge(dir, ruleUp, Vector3.up);
                                    otherText.CheckRulesEdge(dir, ruleDown, Vector3.down);
                                }

                                if (dir == Direction.up)
                                {
                                    otherText.CheckRulesEdge(dir, ruleRight, Vector3.right);
                                    otherText.CheckRulesEdge(dir, ruleLeft, Vector3.left); ;
                                    otherText.CheckRulesEdge(dir, ruleForward, Vector3.forward);
                                    otherText.CheckRulesEdge(dir, ruleBack, Vector3.back);
                                }

                            }
                            else
                            {
                                otherText.CheckRulesList(dir, rule);
                            }
                        }
                    }
                }
            }

        }


       
    }


}
