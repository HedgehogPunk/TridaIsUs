using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Audio;

public class UILogic : MonoBehaviour
{

    public GameObject winPanel, PauseMenu, RulesPanelContent, RulesPanelText;

    public static UnityEvent UnpauseEvent = new UnityEvent();
    public static UnityEvent PauseEvent = new UnityEvent();

    public AudioMixer audioMixer;
    public Slider MusicSlider, SFXSlider;



    private void Awake()
    {
        if (MusicSlider != null && SFXSlider != null)
        {

            MusicSlider.value = Settings.MusicVolume;
            SFXSlider.value = Settings.SoundVolume;

            SetMusicVolume();
            SetSFXVolume();
        }


        if(winPanel != null)
        {
            winPanel.SetActive(false);
        }
        if (PauseMenu != null)
        {
            PauseMenu.SetActive(false);
        }



        isWin.WinEvent.AddListener(ShowWinPanel);
        Rules.PauseEvent.AddListener(Resume);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void SelectLevel()
    {
        SceneManager.LoadScene("SelectLevel");
    }
    public void Resume()
    {
        

        PauseMenu.SetActive(!PauseMenu.activeSelf);
        if(PauseMenu.activeSelf)
        {
            PauseEvent.Invoke();
        }
        else
        {
            UnpauseEvent.Invoke();
        }

        UpdateRulesPanel();

    }

    public void OK(GameObject OkPanel)
    {
        OkPanel.SetActive(false);
        UnpauseEvent.Invoke();
    }

    public void Panel(GameObject Panel)
    {
        Panel.SetActive( ! Panel.activeSelf);
       
    }

    public void SelectLevel(int i)
    {
        SceneManager.LoadScene(i);
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ShowWinPanel()
    {
        winPanel.SetActive(true);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void ChangeLanguage(string language)
    {
        Settings.lang = Settings.DicLangStrings[language];
        Settings.CheckLang();
    }

    public void DirectionArrows()
    {
        Settings.DirectionArrows = ! Settings.DirectionArrows;

        Settings.DirArrowsEvent.Invoke();
    }

    public void UpdateRulesPanel()
    {
        for (int i = RulesPanelContent.transform.childCount - 1; i >= 0; i--)
        {

            Destroy(RulesPanelContent.transform.GetChild(i).gameObject);
        }



        foreach (List<string> rule in Rules.StaticRules)
        {
            GameObject text = Instantiate(RulesPanelText, RulesPanelContent.transform);

            string r = "";

            foreach(string s in rule)
            {
                r += s;
                r += " ";
            }

            text.GetComponent<TextMeshProUGUI>().text = r;
        }
    }

    public void SetMusicVolume()
    {
        float volume = MusicSlider.value;
        audioMixer.SetFloat("music", Mathf.Log10(volume) * 20);
        Settings.MusicVolume = MusicSlider.value;
    }

    public void SetSFXVolume()
    {
        float volume = SFXSlider.value;
        audioMixer.SetFloat("sfx", Mathf.Log10(volume) * 20);
        Settings.SoundVolume = SFXSlider.value;
    }

    public void Grid()
    {
        Settings.GridBool = !Settings.GridBool;

        Settings.GridEvent.Invoke();
    }
}
