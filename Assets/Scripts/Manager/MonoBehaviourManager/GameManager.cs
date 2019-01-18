using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public PlayerManager playerManager;
    public FactoryManager factoryManger;
    public AudioSourceManager audioSourceManger;
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
        audioSourceManger = new AudioSourceManager();
        uiManager = new UIManager();

    }

    public GameObject CreateItem(GameObject itemGo)
    {
        GameObject go = Instantiate(itemGo);
        return go;
    }
}
