using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager
{
    public UIFacade mUIFacade;
    //当前场景所有的UIPanel预制体
    public Dictionary<string, GameObject> currentScenePanelDict;
    public GameManager mGameManager;

    public UIManager()
    {
        mGameManager = GameManager.Instance;
        currentScenePanelDict = new Dictionary<string, GameObject>();
        mUIFacade = new UIFacade(this);
        mUIFacade.currentSceneState = new StartLoadSceneState(mUIFacade);
    }

    public void PushUIPanel(string uiPanelName, GameObject uiPanelGo)
    {
        mGameManager.PushGameObjectToFactory(FactoryType.UIPanelFactory, uiPanelName, uiPanelGo);
    }


    public void ClearDict()
    {
        foreach (var item in currentScenePanelDict)
        {
            PushUIPanel(item.Value.name, item.Value);

        }

        currentScenePanelDict.Clear();
    }
}
