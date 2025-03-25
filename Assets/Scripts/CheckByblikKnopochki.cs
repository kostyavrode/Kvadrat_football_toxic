using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class CheckByblikKnopochki : MonoBehaviour
{
    public GasManager gasik;

    public bool korova;
    
    [FormerlySerializedAs("buttons")] public Button[] knopochki;

    private void Awake()
    {
        if (gasik == null)
        {
            Debug.Log("Gasik null");
        }
        foreach (Button button in knopochki)
        {
            button.interactable = false;
        }
    }

    private void TrogatKorova()
    {
        korova = true;
        LegitKnopochki();
    }
    private void OnEnable()
    {
        TrogatKorova();
    }

    private void LegitKnopochki()
    {
        int disik = PlayerPrefs.GetInt("Levels");
        
        
        if (disik > knopochki.Length)
            
        {
            disik = knopochki.Length;
        }
        // asunc fook 
        if (disik == 0)
        {
            knopochki[0].interactable = true;
        }
        
        for (int i = 0; i < disik; i++)
        {
            korova = false;
            knopochki[i].interactable = true;
        }
    }
}
