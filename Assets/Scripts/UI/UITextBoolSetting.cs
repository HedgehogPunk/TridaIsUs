using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UITextBoolSetting : MonoBehaviour
{
    public Dictionary<Language, string> FalseWords = new Dictionary<Language, string>();
    public List<string> FalseTexts;
    public List<Language> languages;

    public Dictionary<Language, string> TrueWords = new Dictionary<Language, string>();
    public List<string> TrueTexts;


    public TextMeshProUGUI textMesh;

    public settingBools settingBool;

    private void Awake()
    {

        Settings.LangChangedEvent.AddListener(SetText);

        for (int i = 0; i < languages.Count; i++)
        {
            FalseWords.Add(languages[i], FalseTexts[i]);

            TrueWords.Add(languages[i], TrueTexts[i]);
        }
    }

    void Start()
    {
       if(Settings.DirectionArrows)
       {
            textMesh.text = TrueWords[Settings.lang];
       }
        else
        {
            textMesh.text = FalseWords[Settings.lang];
        }


    }

    public void SetText()
    {
        if (settingBool == settingBools.directionArrows)
        {
            if (Settings.DirectionArrows)
            {
                textMesh.text = TrueWords[Settings.lang];
            }
            else
            {
                textMesh.text = FalseWords[Settings.lang];
            }
        }
        if (settingBool == settingBools.grid)
        {
            if (Settings.GridBool)
            {
                textMesh.text = TrueWords[Settings.lang];
            }
            else
            {
                textMesh.text = FalseWords[Settings.lang];
            }
        }
    }

}

public enum settingBools
{
    directionArrows,
    grid
}
