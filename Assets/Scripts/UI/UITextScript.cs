using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UITextScript : MonoBehaviour
{
   


    public Dictionary<Language, string> words = new Dictionary<Language, string>();
    public List<string> texts;
    public List<Language> languages;

   


    public TextMeshProUGUI textMesh;



    private void Awake()
    {

        Settings.LangChangedEvent.AddListener(SetText);

        for (int i = 0; i < languages.Count; i++)
        {
            words.Add(languages[i], texts[i]);
        }
    }

    void Start()
    {
      
        textMesh.text = words[Settings.lang];

    }

    public void SetText()
    {
        textMesh.text = words[Settings.lang];
    }

}
