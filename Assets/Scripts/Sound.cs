using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour
{
    public AudioSource music,step, win, death;


    public static Sound instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }



        YouManager.StepEvent.AddListener(Step);

        isWin.WinEvent.AddListener(Win);

        isDeath.DeathEvent.AddListener(Death);
    }


    public void Step()
    {
        step.Play();
    }

    public void Win()
    {
        win.Play();
    }

    public void Death()
    {
        death.Play();
    }
}
