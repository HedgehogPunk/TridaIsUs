using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Settings : MonoBehaviour
{
    public static Language lang;

    public static  Dictionary<string, Language> DicLangStrings = new Dictionary<string, Language>();
    public List<string> DicStrings = new List<string>();
    public List<Language> DicLang = new List<Language>();

    public static bool DirectionArrows = true, GridBool = true;

    public static UnityEvent LangChangedEvent = new UnityEvent();

    public static UnityEvent DirArrowsEvent = new UnityEvent();

    public static UnityEvent GridEvent = new UnityEvent();

    public static float MusicVolume = 1, SoundVolume = 1;

    private void Awake()
    {
        for(int i=0; i < DicLang.Count; i++)
        {
            if (!DicLangStrings.ContainsKey(DicStrings[i]))
            {
                DicLangStrings.Add(DicStrings[i], DicLang[i]);
            }
        }
    }

    

    public static void CheckLang()
    {

        LangChangedEvent.Invoke();

    }
}

public enum Language
{
    English,
    Russian
}
