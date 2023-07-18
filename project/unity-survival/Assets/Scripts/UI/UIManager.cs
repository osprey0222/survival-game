using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UIManager
{
    private static Dictionary<string, GameObject> m_UIDic = new Dictionary<string, GameObject>();
    private static Canvas m_Canvas;
    private static string m_UIPath = "Prefab/";

    public static void Init()
    {
        m_Canvas = GameObject.FindObjectOfType<Canvas>();
    }

    public static GameObject Show(string uiName)
    {
        GameObject uiObj = null;
        if (m_UIDic.ContainsKey(uiName))
        {
            m_UIDic[uiName].GetComponent<RectTransform>().SetAsLastSibling();
            m_UIDic[uiName].gameObject.SetActive(true);
        }
        else
        {
            uiObj = Resources.Load<UIBase>(m_UIPath + uiName).gameObject;
            uiObj = GameObject.Instantiate(uiObj) as GameObject;

            uiObj.transform.SetParent(m_Canvas.gameObject.transform);;
            uiObj.transform.localPosition = Vector3.one;
            uiObj.transform.localScale = Vector3.one;
            uiObj.GetComponent<RectTransform>().sizeDelta = Vector2.zero;
            uiObj.SetActive(true);
            m_UIDic.Add(uiName, uiObj);

        }
        return uiObj;
    }

    private static void HideUI(string uiName)
    {
        m_UIDic[uiName].gameObject.SetActive(false);
    }

    private static void ShowUI(string uiName)
    {
        m_UIDic[uiName].gameObject.SetActive(true);
    }

    public static void ClearUIs()
    {
        foreach (var item in m_UIDic)
        {
            GameObject.Destroy(item.Value);
        }
        m_UIDic.Clear();
    }
}
