using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager 
{
    public UIFacade mUIFacade;
    //当前场景所有的UIPanel预制体
    public Dictionary<string, GameObject> curentScenePanelDict;
    public GameManager mGameManager;

    public UIManager()
    {
        mGameManager = GameManager.Instance;
        curentScenePanelDict = new Dictionary<string, GameObject>();
        mUIFacade = new UIFacade();

    }
}
