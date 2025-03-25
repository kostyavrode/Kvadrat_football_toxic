using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class SmegmaLover : MonoBehaviour
{
    [FormerlySerializedAs("cameraPos")] public Transform transformForCamera;
    
    
    [FormerlySerializedAs("animator")] public Animator animatronick;

    public void Up()
    {
        animatronick.SetTrigger("head");
    }

    public void Down()
    {
        animatronick.SetTrigger("leg");
    }
}
