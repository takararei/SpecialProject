using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseFactory : IBaseFactory
{
    //游戏物体资源
    protected Dictionary<string, GameObject> factoryDict = new Dictionary<string, GameObject>();
    //对象池 游戏物体对象
    //对象池字典
    protected Dictionary<string, Stack<GameObject>> objectPoolDict = new Dictionary<string, Stack<GameObject>>();

    protected string loadPath;
    public BaseFactory()
    {
        loadPath = "Prefabs/";
    }

    public GameObject GetItem(string itemName)
    {
        GameObject itemGo = null;
        if (objectPoolDict.ContainsKey(itemName))
        {
            if (objectPoolDict[itemName].Count == 0)
            {
                GameObject go = GetResource(itemName);
                itemGo = GameManager.Instance.CreateItem(go);
            }
            else
            {
                itemGo = objectPoolDict[itemName].Pop();
                itemGo.SetActive(true);
            }
        }
        else
        {
            objectPoolDict.Add(itemName, new Stack<GameObject>());
            GameObject go = GetResource(itemName);
            itemGo = GameManager.Instance.CreateItem(go);
        }

        if (itemGo == null)
        {
            Debug.Log(itemName + "实例获取失败");
        }

        return itemGo;
    }

    private GameObject GetResource(string itemName)
    {
        GameObject itemGo = null;
        string itemLoadPath = loadPath + itemName;
        if (factoryDict.ContainsKey(itemName))
        {
            itemGo = factoryDict[itemName];

        }
        else
        {
            itemGo = Resources.Load<GameObject>(itemLoadPath);
            factoryDict.Add(itemName, itemGo);
        }

        if (itemGo == null)
        {
            Debug.Log(itemName + "资源获取失败，失败路径" + itemLoadPath);
        }
        return itemGo;
    }

    public void PushItem(string itemName, GameObject item)
    {
        item.SetActive(false);
        item.transform.SetParent(GameManager.Instance.transform);
        if (objectPoolDict.ContainsKey(itemName))
        {
            objectPoolDict[itemName].Push(item);
        }
        else
        {
            Debug.Log("字典没有这样的栈" + itemName);
        }

    }

}
