using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class SceneInstallers : MonoInstaller
{
    public CheckByblikKnopochki levelChecker;
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<CheckByblikKnopochki>().FromComponentInHierarchy(levelChecker).AsSingle();
    }
}
