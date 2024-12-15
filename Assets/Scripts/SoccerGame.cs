using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using PixelSunsetStudio;
using Random = UnityEngine.Random; // Import DOTween namespace

public class SoccerGame : MonoBehaviour
{
    public GameObject ball;
    public List<GameObject> players; // List of all players
    public float ballTravelTime = 2f; // Time for the ball to travel between players
    public Camera gameCamera; // Camera to focus on players
    public float cameraMoveDuration = 1f; // Duration for camera movement

    private GameObject currentTarget;
    public Transform center;
    private Vector3 targetPosition;
    private bool isBallFlying;
    private bool correctInput;
    
    public int successPasses;
    public int targetPasses;

    void OnEnable()
    {
        gameCamera=Camera.main;
        if (players.Count > 0)
        {
            PassBallToRandomPlayer();
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PassBallToRandomPlayer();
            Debug.Log(Time.timeScale);
        }
        if (isBallFlying)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow) && targetPosition == currentTarget.transform.Find("HeadTarget").position)
            {
                correctInput = true;
                currentTarget.GetComponent<Footballer>().Head();
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow) && targetPosition == currentTarget.transform.Find("FeetTarget").position)
            {
                correctInput = true;
                currentTarget.GetComponent<Footballer>().Leg();
            }
        }
    }

    public void HeadAttack()
    {
        {
            if (targetPosition == currentTarget.transform.Find("HeadTarget").position)
            {
                correctInput = true;
                currentTarget.GetComponent<Footballer>().Head();
            }
        }
    }

    public void LegAttack()
    {
        {
            if (targetPosition == currentTarget.transform.Find("FeetTarget").position)
            {
                correctInput = true;
                currentTarget.GetComponent<Footballer>().Leg();
            }
        }
    }
    public void SetTargetPasses(int passes)
    {
        targetPasses=passes;
        UITemplate.instance.ShowPasses(targetPasses.ToString(),successPasses.ToString());
        SimpleSwipeDetector.OnSwipeUp += HeadAttack;
        SimpleSwipeDetector.OnSwipeDown += LegAttack;
    }
    private void LookAtCenter()
    {
        gameCamera.transform.LookAt(center.position);
    }
    void PassBallToRandomPlayer()
    {
        isBallFlying = false;
        correctInput = false;

        // Choose a random player from the list
        GameObject previousTarget = currentTarget;
        //if (previousTarget != null)
            //previousTarget.GetComponent<Footballer>().Pass();
        currentTarget = players[Random.Range(0, players.Count)];
        while (previousTarget == currentTarget)
        {
            currentTarget = players[Random.Range(0, players.Count)];
        }


        // Adjust camera to focus on the new target player
        FocusCameraOnPlayer(currentTarget);

        // Choose a random target on the player (HeadTarget or FeetTarget)
        Transform headTarget = currentTarget.transform.Find("HeadTarget");
        Transform feetTarget = currentTarget.transform.Find("FeetTarget");
        targetPosition = Random.value > 0.5f ? headTarget.position : feetTarget.position;

        StartCoroutine(MoveBallToTarget(targetPosition == headTarget.position));
    }

    void FocusCameraOnPlayer(GameObject player)
    {
        if (gameCamera != null)
        {
            Transform cameraPos = player.GetComponent<Footballer>().cameraPos;
            Vector3 targetCameraPosition = player.transform.position +player.transform.forward*(-3)  
                                         + transform.up*2+gameCamera.transform.right*(-3);
            gameCamera.transform.DOMove(cameraPos.position, cameraMoveDuration)
                .SetEase(Ease.InOutQuad).OnUpdate(LookAtCenter);
            //gameCamera.transform.DOLookAt(center.position, cameraMoveDuration).SetEase(Ease.InOutQuad);
        }
    }




    IEnumerator MoveBallToTarget(bool isHeadTarget)
    {
        isBallFlying = true;
        Vector3 startPosition = ball.transform.position;
        float elapsedTime = 0f;

        if (isHeadTarget)
        {
            Vector3 midPoint = (startPosition + targetPosition) / 2 + Vector3.up * 5f; // Create a curve upwards
            while (elapsedTime < ballTravelTime)
            {
                float t = elapsedTime / ballTravelTime;
                Vector3 curvePosition = Vector3.Lerp(Vector3.Lerp(startPosition, midPoint, t), Vector3.Lerp(midPoint, targetPosition, t), t);
                ball.transform.position = curvePosition;
                elapsedTime += Time.deltaTime;
                yield return null;
            }
        }
        else
        {
            while (elapsedTime < ballTravelTime)
            {
                ball.transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / ballTravelTime);
                elapsedTime += Time.deltaTime;
                yield return null;
            }
        }

        ball.transform.position = targetPosition;
        isBallFlying = false;

        if (correctInput)
        {
            successPasses += 1;
            UITemplate.instance.ShowPasses(targetPasses.ToString(),successPasses.ToString());
            if (successPasses >= targetPasses)
            {
                GameOver(true);
            }
            else
            {
                PassBallToRandomPlayer();
            }
        }
        else
        {
            GameOver(false);
        }
    }

    void GameOver(bool isWin)
    {
        Debug.Log("GameOver");
        UITemplate.instance.EndGame(isWin);
        successPasses = 0;
        SimpleSwipeDetector.OnSwipeUp -= HeadAttack;
        SimpleSwipeDetector.OnSwipeDown -= LegAttack;
    }

    private void OnDisable()
    {
        SimpleSwipeDetector.OnSwipeUp -= HeadAttack;
        SimpleSwipeDetector.OnSwipeDown -= LegAttack;
    }
}