using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
    
public class InjectVars : MonoInstaller
{

    // public Player Player;
    public SceneScript SceneScript;
    
    public override void InstallBindings()
    {
        // Привязываем класс Player к себе же
        // Container.Bind<Player>().FromNew().AsSingle().NonLazy();
        Container.Bind<ISceneScript>().To<SceneScript>().FromInstance(SceneScript).AsSingle().NonLazy();
        
    }
}