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

//        ShowMoney(TelepyzikInformation._instance.TelepyzikMoney().ToString());

        ShowMoney(InfoHandler.instance.GetPlayerMoney().ToString());

    }

    public void ShowMoney(string data)
    {
        moneyBar.text = data;
    }
    public void StartGame()
    {
        GagaNachalnik._instance.ChangeGameState(GameState.PLAYING);
    }
    public void PauseGame()
    {
        GagaNachalnik._instance.ChangeGameState(GameState.PAUSE);
    }
    public void UnPauseGame()
    {
        GagaNachalnik._instance.ChangeGameState(GameState.PLAYING);
    }
    public void EndGame(bool isWin)
    {
        GagaNachalnik._instance.ChangeGameState(GameState.END);
        ShowMoney(InfoHandler.instance.GetPlayerMoney().ToString());

        inGamePanel.SetActive(false);
        if (isWin)
        {
            winPanel.SetActive(true);
            PlayerPrefs.SetInt("Levels",PlayerPrefs.GetInt("Levels")+1);

//            TelepyzikInformation._instance.HelpTelepyzik(10);
            winningsBar.text="+10";
//            ShowMoney(TelepyzikInformation._instance.TelepyzikMoney().ToString());

            InfoHandler.instance.AddMoney(10);
            winningsBar.text="+10";
            ShowMoney(InfoHandler.instance.GetPlayerMoney().ToString());

        }
        else
        {
            losePanel.SetActive(true);
        }
//        ShowMoney(TelepyzikInformation._instance.TelepyzikMoney().ToString());

        GagaNachalnik._instance.ChangeGameState(GameState.PLAYING);
    }

    public void ShowPasses(string target, string success)
    {
        passesBar.text = success + " | " + target;
    }
    public void RestartGame()
    {

//        GasManager._instance.SetNewState(Karabybliki.renewkarabyblik);

        GagaNachalnik._instance.ChangeGameState(GameState.RESTART);

    }
    public void ExitApp()
    {
        Application.Quit();
    }
}
