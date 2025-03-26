using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class KartinkaKakashki : MonoBehaviour
{
    public static KartinkaKakashki instance;

    public float levell;

    [FormerlySerializedAs("inGamePanel")] [SerializeField] private GameObject poigratOkno;
    [FormerlySerializedAs("losePanel")] [SerializeField] private GameObject proebali;
    [FormerlySerializedAs("winPanel")] [SerializeField] private GameObject goidaPanel;
    
    [FormerlySerializedAs("moneyBar")] [SerializeField] private TMP_Text denezhkiBar;
    [FormerlySerializedAs("passesBar")] [SerializeField] private TMP_Text yaNePonyal;
    [FormerlySerializedAs("winningsBar")] [SerializeField] private TMP_Text GGGoidaPanel;
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {

        levell = PlayerPrefs.GetFloat("levell");
//        ShowMoney(TelepyzikInformation._instance.TelepyzikMoney().ToString());

        PokazhiDenezhki(InfoHandler.instance.GetPlayerMoney().ToString());

    }

    public void PokazhiDenezhki(string data)
    {
        denezhkiBar.text = data;
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
        PokazhiDenezhki(InfoHandler.instance.GetPlayerMoney().ToString());

        poigratOkno.SetActive(false);
        if (isWin)
        {
            goidaPanel.SetActive(true);
            PlayerPrefs.SetInt("Levels",PlayerPrefs.GetInt("Levels")+1);

//            TelepyzikInformation._instance.HelpTelepyzik(10);
            GGGoidaPanel.text="+10";
//            ShowMoney(TelepyzikInformation._instance.TelepyzikMoney().ToString());

            InfoHandler.instance.AddMoney(10);
            GGGoidaPanel.text="+10";
            PokazhiDenezhki(InfoHandler.instance.GetPlayerMoney().ToString());

        }
        else
        {
            proebali.SetActive(true);
        }
//        ShowMoney(TelepyzikInformation._instance.TelepyzikMoney().ToString());

        GagaNachalnik._instance.ChangeGameState(GameState.PLAYING);
    }

    public void ShowPasses(string target, string success)
    {
        yaNePonyal.text = success + " | " + target;
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
