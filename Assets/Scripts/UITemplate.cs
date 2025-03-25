using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UITemplate : MonoBehaviour
{
    public static UITemplate instance;

    [SerializeField] private GameObject inGamePanel;
    [SerializeField] private GameObject losePanel;
    [SerializeField] private GameObject winPanel;
    
    [SerializeField] private TMP_Text moneyBar;
    [SerializeField] private TMP_Text passesBar;
    [SerializeField] private TMP_Text winningsBar;
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        ShowMoney(TelepyzikInformation._instance.TelepyzikMoney().ToString());
    }

    public void ShowMoney(string data)
    {
        moneyBar.text = data;
    }
    public void StartGame()
    {
        GasManager._instance.SetNewState(Karabybliki.kaarabyblik);
    }
    public void PauseGame()
    {
        GasManager._instance.SetNewState(Karabybliki.nekarabyblik);
    }
    public void UnPauseGame()
    {
        GasManager._instance.SetNewState(Karabybliki.kaarabyblik);
    }
    public void EndGame(bool isWin)
    {
        GasManager._instance.SetNewState(Karabybliki.plakiplakikarabybliki);
        ShowMoney(TelepyzikInformation._instance.TelepyzikMoney().ToString());
        inGamePanel.SetActive(false);
        if (isWin)
        {
            winPanel.SetActive(true);
            PlayerPrefs.SetInt("Levels",PlayerPrefs.GetInt("Levels")+1);
            TelepyzikInformation._instance.HelpTelepyzik(10);
            winningsBar.text="+10";
            ShowMoney(TelepyzikInformation._instance.TelepyzikMoney().ToString());
        }
        else
        {
            losePanel.SetActive(true);
        }
    }

    public void ShowPasses(string target, string success)
    {
        passesBar.text = success + " | " + target;
    }
    public void RestartGame()
    {
        GasManager._instance.SetNewState(Karabybliki.renewkarabyblik);
    }
    public void ExitApp()
    {
        Application.Quit();
    }
}
