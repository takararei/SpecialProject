using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public PlayerManager playerManager;
    public FactoryManager factoryManger;
    public AudioSourceManager audioSourceManager;
    public UIManager uiManager;

    private static GameManager _instance;

    public static GameManager Instance
    {
        get
        {
            return _instance;
        }
    }

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        _instance = this;
        playerManager = new PlayerManager();
        factoryManger = new FactoryManager();
        audioSourceManager = new AudioSourceManager();
        uiManager = new UIManager();

    }

    public GameObject CreateItem(GameObject itemGo)
    {
        GameObject go = Instantiate(itemGo);
        return go;
    }

    public Sprite GetSprite(string resourcePath)
    {
        return factoryManger.spriteFactory.GetSingleResource(resourcePath);
    }

    public AudioClip GetAudioClip(string resourcePath)
    {
        return factoryManger.auidoClipFactory.GetSingleResource(resourcePath);
    }

    public RuntimeAnimatorController GetRuntimeAnimatorController(string resourcePath)
    {
        return factoryManger.runtimeAnimatorFactory.GetSingleResource(resourcePath);
    }

    public GameObject GetGameObjectResource(FactoryType factoryType,string resourcePath)
    {
        return factoryManger.factoryDict[factoryType].GetItem(resourcePath);
    }

    public void PushGameObjectToFactory(FactoryType factoryType, string resourcePath,GameObject itemGo)
    {
        factoryManger.factoryDict[factoryType].PushItem(resourcePath, itemGo);
    }

}
