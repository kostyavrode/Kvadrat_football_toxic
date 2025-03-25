using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerFoot : MonoBehaviour
{
    [FormerlySerializedAs("cameraPos")] public Transform cam;
    [FormerlySerializedAs("animator")] public Animator anim;

    public void Up()
    {
        anim.SetTrigger("head");
    }

    public void Down()
    {
        anim.SetTrigger("leg");
    }
}
