using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FactoryManager
{
    public Dictionary<FactoryType, IBaseFactory> factoryDict = new Dictionary<FactoryType, IBaseFactory>();
    public AudioClipFactory auidoClipFactory;
    public SpriteFactory spriteFactory;
    public RuntimeAnimatorControllerFactory runtimeAnimatorFactory;

    public FactoryManager()
    {
        factoryDict.Add(FactoryType.UIPanelFactory, new UIPanelFactory());
        factoryDict.Add(FactoryType.UIFactory, new UIFactory());
        factoryDict.Add(FactoryType.GameFactory, new GameFactory());
        auidoClipFactory = new AudioClipFactory();
        spriteFactory = new SpriteFactory();
        runtimeAnimatorFactory = new RuntimeAnimatorControllerFactory();
    }

}
