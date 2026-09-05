using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    private void Awake()
    {
        Settings.GridEvent.AddListener(SetGrid);

        SetGrid();
    }


    public void SetGrid()
    {
        gameObject.SetActive(Settings.GridBool);
    }
}
