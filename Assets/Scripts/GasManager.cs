using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public enum Gass
{
    ROUND,
    BOTTOM,
    LEFT,
    RIGHT,
}

public class GasManager : MonoBehaviour
{
    public static GasManager _instance;
    
    [FormerlySerializedAs("levelPrefab")] public SoccerGame soccerPrefab;

    private float grill;
    
    public SoccerGame gameSocc;
    
    
    
    [FormerlySerializedAs("gameState")] public Karabybliki currentState;
    
    //asdasasasasdasdasdas
    
    [FormerlySerializedAs("increaseTimeScale")] public bool isRaisingScale;

    private int scaler;
    
    private float nowTimeScalee;

    private bool inc;
    
    private int previousPassTerget;
    
    private void Update()
    {
        if (currentState==Karabybliki.kaarabyblik && isRaisingScale)
        {
            nowTimeScalee += 0.001f;
            Time.timeScale = nowTimeScalee;
        }
    }
    public void Awake()
    {
        _instance = this;
    }

    public void Start()
    {
        SetNewState(Karabybliki.First);
        BuffKarabyblik();
    }


    private void BuffKarabyblik()
    {
        inc = true;
    }

    public void SetNewState(Karabybliki newKarabybliki)
    {
        currentState = newKarabybliki;
        switch(newKarabybliki)
        {
            case Karabybliki.First:
                {
                    KartinkaKakashki.instance.PokazhiDenezhki(TelepyzikInformation._instance.TelepyzikMoney().ToString());
                    nowTimeScalee=1f;
                    Time.timeScale = 1;
                    break;
                }
                case Karabybliki.kaarabyblik:
                {
                    if (!gameSocc)
                    {
                        StartKarabybliki();
                    }
                    Time.timeScale = 1;
                    break;
                }
                case Karabybliki.nekarabyblik:
                {
                    Time.timeScale = 0;
                    break;
                }
            case Karabybliki.plakiplakikarabybliki:
                {
                    break;
                }
                case Karabybliki.renewkarabyblik:
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                    Rerekarabyblik();
                    break;
                }
        }    
    }

    public void Rerekarabyblik()
    {
        FinishKarabybliki();
        StartKarabybliki();
        NewPointByblik(previousPassTerget);
    }

    public void NewPointByblik(int passes)
    {
        gameSocc.SetTargetPasses(passes);
        previousPassTerget=passes;
    }
    
    
    public void StartKarabybliki()
    {
        gameSocc = Instantiate(soccerPrefab);
    }

    public void FinishKarabybliki()
    {
        SetNewState(Karabybliki.plakiplakikarabybliki);
        Destroy(gameSocc.gameObject);
    }
}
public enum Karabybliki
{
    First,
    kaarabyblik,
    nekarabyblik,
    plakiplakikarabybliki,
    renewkarabyblik
}