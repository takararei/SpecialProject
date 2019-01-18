using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteFactory : IBaseResourceFactory<Sprite>
{
    protected Dictionary<string, Sprite> factoryDict = new Dictionary<string, Sprite>();
    protected string LoadPath;

    public SpriteFactory()
    {
        LoadPath = "Pictures/";
    }

    public Sprite GetSingleResource(string resourcePath)
    {
        Sprite itemGo = null;
        string itemLoadPath = LoadPath + resourcePath;
        if (factoryDict.ContainsKey(resourcePath))
        {
            itemGo = factoryDict[resourcePath];
        }
        else
        {
            itemGo = Resources.Load<Sprite>(itemLoadPath);
            factoryDict.Add(resourcePath, itemGo);

        }

        if (itemGo == null)
        {
            Debug.Log(resourcePath + "获取失败，路径有误" + itemLoadPath);
        }

        return itemGo;
    }
}
