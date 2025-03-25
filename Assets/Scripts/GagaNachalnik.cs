using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class GagaNachalnik : MonoBehaviour
{
    public static GagaNachalnik _instance;
    
    [FormerlySerializedAs("levelPrefab")] public SoccerGame noskiNaiti;
    
    private SoccerGame currentNosok;
    
    [FormerlySerializedAs("gameState")] public GameState Byli;
    
    
    [FormerlySerializedAs("increaseTimeScale")] public bool PodnyatByli;
    
    
    private float ByliVremya;

    private int lastTargetPasses;
    
    private void Awake()
    {
        _instance = this;
    }

    private void Start()
    {
        ChangeGameState(GameState.MENU);
    }

    private void Update()
    {
        if (Byli==GameState.PLAYING && PodnyatByli)
        {
            ByliVremya += 0.001f;
            Time.timeScale = ByliVremya;
        }
    }

    public void ChangeGameState(GameState newState)
    {
        Byli = newState;
        switch(newState)
        {
            case GameState.MENU:
                {
                    UITemplate.instance.ShowMoney(InfoHandler.instance.GetPlayerMoney().ToString());
                    ByliVremya=1f;
                    Time.timeScale = 1;
                    break;
                }
                case GameState.PLAYING:
                {
                    if (!currentNosok)
                    {
                        StartGame();
                    }
                    Time.timeScale = 1;
                    break;
                }
                case GameState.PAUSE:
                {
                    Time.timeScale = 0;
                    break;
                }
            case GameState.END:
                {
                    break;
                }
                case GameState.RESTART:
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                    break;
                }
        }    
    }

    public void RestartGame()
    {
        EndGame();
        StartGame();
        SetTargetPasses(lastTargetPasses);
    }

    public void SetTargetPasses(int passes)
    {
        currentNosok.SetTargetPasses(passes);
        lastTargetPasses=passes;
    }
    public void StartGame()
    {
        currentNosok = Instantiate(noskiNaiti);
    }

    public void EndGame()
    {
        ChangeGameState(GameState.END);
        Destroy(currentNosok.gameObject);
    }
}
public enum GameState
{
    MENU,
    PLAYING,
    PAUSE,
    END,
    RESTART
}