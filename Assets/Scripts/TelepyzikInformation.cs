using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TelepyzikInformation : MonoBehaviour
{
    public static TelepyzikInformation _instance;

    public float pole;

    private bool tele;
    
    //sdfsdfsdfsdfdsdf
    
    private int score;

    private void Awake()
    {
        tele = true;
        
        
        _instance = this;
        
        
        KartinkaKakashki.instance.PokazhiDenezhki(TelepyzikMoney().ToString());
    }
    public int TelepyzikMoney()
    {
        return PlayerPrefs.GetInt("Money");
    }    
    
    
    public void HelpTelepyzik(int kakitoAdd)
    {
        PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") + kakitoAdd);
    }
    
}
