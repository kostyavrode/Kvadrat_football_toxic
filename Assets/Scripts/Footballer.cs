using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footballer : MonoBehaviour
{
    public Transform cameraPos;
    public Animator animator;

    public void Pass()
    {
        animator.SetTrigger("pass");
    }

    public void Head()
    {
        animator.SetTrigger("head");
    }

    public void Leg()
    {
        animator.SetTrigger("leg");
    }
}
