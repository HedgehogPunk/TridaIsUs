using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EdgeOfText : MonoBehaviour
{

    public Material material;


    public Dictionary<Language, string> words = new Dictionary<Language, string>();
    public List<string> texts;
    public List<Language> languages;

    public Color textColor;

    
    public TextMeshPro textMesh;



    private void Awake()
    {
        for(int i = 0; i < languages.Count; i++)
        {
            words.Add(languages[i], texts[i]);
        }
    }

    void Start()
    {
        GetComponent<MeshRenderer>().material = material;

        textMesh.text = words[Settings.lang];

       textMesh.color = textColor;
    }

    
}
