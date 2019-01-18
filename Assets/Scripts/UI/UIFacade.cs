using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
/// <summary>
/// 上层与Manager做交互，下层与各面板交互
/// </summary>
public class UIFacade
{
    //上层
    private UIManager mUIManager;
    private GameManager mGameManager;
    private AudioSourceManager mAudioSourceManager;
    private PlayerManager mPlayerManager;

    //下层 面板字典
    public Dictionary<string, IBasePanel> currentScenePanelDict = new Dictionary<string, IBasePanel>();

    //其他成员变量
    private GameObject mask;
    private Image maskImage;
    public Transform canvasTransform;
    //场景状态
    public IBaseSceneState currentSceneState;
    public IBaseSceneState lastSceneState;
    public UIFacade(UIManager uiManager)
    {
        mGameManager = GameManager.Instance;
        mPlayerManager = mGameManager.playerManager;
        mUIManager = uiManager;
        mAudioSourceManager = mGameManager.audioSourceManager;

    }

    public void InitMask()
    {
        canvasTransform = GameObject.Find("Canvas").transform;
        mask = CreateUIAndSetUIPosition("Img_Mask");
        maskImage = mask.GetComponent<Image>();
    }

    //改变当前场景的状态
    public void ChangeSceneState(IBaseSceneState baseSceneState)
    {
        lastSceneState = currentSceneState;
        ShowMask();
        currentSceneState = baseSceneState;
    }
    //显示遮罩
    public void ShowMask()
    {
        Tween t = DOTween.To(() => maskImage.color, toColor => maskImage.color = toColor, new Color(0, 0, 0, 1), 2f);
        t.OnComplete(ExitSceneComplete);
    }

    public void HideMask()
    {
        DOTween.To(() => maskImage.color, toColor => maskImage.color = toColor, new Color(0, 0, 0, 0), 1f);
    }

    //离开当前场景的方法
    private void ExitSceneComplete()
    {
        lastSceneState.ExitScene();
        currentSceneState.EnterScene();
    }

    //UIPanel部分 实例化所有面板的类，存到字典
    public void InitDict()
    {
        foreach (var item in mUIManager.currentScenePanelDict)
        {
            item.Value.transform.SetParent(canvasTransform);
            item.Value.transform.localPosition = Vector3.zero;
            item.Value.transform.localScale = Vector3.one;
            IBasePanel basePanel = item.Value.GetComponent<IBasePanel>();
            if (basePanel == null)
            {
                Debug.Log("获取面板上IBasePanel脚本失败");
            }
            basePanel.InitPanel();
            currentScenePanelDict.Add(item.Key, basePanel);
        }
    }

    //清空UIPanel字典

    public void ClearDict()
    {
        currentScenePanelDict.Clear();
        mUIManager.ClearDict();
    }

    //面板的游戏物体添加到UIManager字典
    public void AddPanelToDict(string uiPanelName)
    {
        mUIManager.currentScenePanelDict.Add(uiPanelName, GetGameObjectResource(FactoryType.UIPanelFactory, uiPanelName));
    }

    //UI部分
    public GameObject CreateUIAndSetUIPosition(string uiName)
    {
        GameObject itemGo = GetGameObjectResource(FactoryType.UIFactory, uiName);
        itemGo.transform.SetParent(canvasTransform);
        itemGo.transform.localPosition = Vector3.zero;
        itemGo.transform.localScale = Vector3.one;

        return itemGo;
    }


    public Sprite GetSprite(string resourcePath)
    {
        return mGameManager.GetSprite(resourcePath);
    }

    public AudioClip GetAudioClip(string resourcePath)
    {
        return mGameManager.GetAudioClip(resourcePath);
    }

    public RuntimeAnimatorController GetRuntimeAnimatorController(string resourcePath)
    {
        return mGameManager.GetRuntimeAnimatorController(resourcePath);
    }

    public GameObject GetGameObjectResource(FactoryType factoryType, string resourcePath)
    {
        return mGameManager.GetGameObjectResource(factoryType, resourcePath);
    }

    public void PushGameObjectToFactory(FactoryType factoryType, string resourcePath, GameObject itemGo)
    {
        mGameManager.PushGameObjectToFactory(factoryType, resourcePath, itemGo);
    }
}
