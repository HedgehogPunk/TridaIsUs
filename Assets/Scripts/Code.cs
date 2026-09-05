using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Code : MonoBehaviour
{
    public TriggersHolder th;

    public TriggersHolder thText;

    public GameBlock gameBlock;

    public TextBlock textBlock;

    public GameObject TextPrefab;

    public Collider PlayerLayerCollider;


    public Dictionary<string, Rule> ActionDic = new Dictionary<string, Rule>();
    public List<string> DicStrings;
    public List<Rule> DicScripts;

   


    private void Awake()
    {
        FillDic();

       

        
    }

  

   



    public void FillDic()
    {
       

        for (int i = 0; i < DicStrings.Count; i++)
        {
            ActionDic.Add(DicStrings[i], DicScripts[i]);
        }

    }
}
