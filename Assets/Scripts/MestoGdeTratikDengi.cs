using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class MestoGdeTratikDengi : MonoBehaviour
{
    [FormerlySerializedAs("costs")] public int[] skolkoMonetokOdat;
    
    [FormerlySerializedAs("buttons")] public Button[] knopochki;

    [FormerlySerializedAs("notenoug")] public GameObject votBlin;

    private bool a;

    private void Awake()
    {
        a = true;
    }

    private void OnEnable()
    {
        votBlin.SetActive(false);
    }
    public void OdatMonetki(int skolkoMonetok)
    {
        if (PlayerPrefs.GetInt("Money") >= skolkoMonetokOdat[skolkoMonetok])
        {
            PlayerPrefs.SetInt("CurrentSkin", skolkoMonetok);
            
            //sdfsdfsdfsdfsd
            
            Debug.Log(skolkoMonetok);
            PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") - skolkoMonetokOdat[skolkoMonetok]);
            
            
            knopochki[skolkoMonetok].interactable = false;
        }
        else
        {
            votBlin.SetActive(true);
        }
    }
}