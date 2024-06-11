using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavingWrapper : MonoBehaviour
{
    const string defaultSaveFile = "save";

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            Load();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            Save();
        }
    }

    private void Save()
    {
        GetComponent<JsonSavingSystem>().Save(defaultSaveFile);
    }

    private void Load()
    {
        GetComponent<JsonSavingSystem>().Load(defaultSaveFile);
    }
}
