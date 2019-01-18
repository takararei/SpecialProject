using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartLoadSceneState : BaseSceneState
{
    public StartLoadSceneState(UIFacade uiFacade):base(uiFacade)
    {

    }

    public override void EnterScene()
    {
        mUIFacade.AddPanelToDict("StartLoadPanel");
        base.EnterScene();

    }
}

