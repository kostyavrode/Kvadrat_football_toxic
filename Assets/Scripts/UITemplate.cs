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
    private void Awake()
    {
        instance = this;
    }
    public void ShowMoney(string data)
    {
        moneyBar.text = data;
    }
    public void StartGame()
    {
        GameManager.instance.ChangeGameState(GameState.PLAYING);
    }
    public void PauseGame()
    {
        GameManager.instance.ChangeGameState(GameState.PAUSE);
    }
    public void UnPauseGame()
    {
        GameManager.instance.ChangeGameState(GameState.PLAYING);
    }
    public void EndGame()
    {
        GameManager.instance.ChangeGameState(GameState.END);
        ShowMoney(Player.instance.GetPlayerMoney().ToString());
        losePanel.SetActive(true);
        inGamePanel.SetActive(false);
    }
    public void RestartGame()
    {
        GameManager.instance.ChangeGameState(GameState.RESTART);
    }
    public void ExitApp()
    {
        Application.Quit();
    }
}
