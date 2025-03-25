using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bootstrapper : MonoBehaviour
{
    public GameObject _translatorObject;
    public GameObject _level;
    public GameObject _ui;

    private void Awake()
    {
        _translatorObject.SetActive(true);
        _level.SetActive(true);
        _ui.SetActive(true);
    }
}
