using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;


public class NPC_Manager : Singleton<NPC_Manager>
{
    public List<NPC_Position> npcDateList = new List<NPC_Position>();
    /// <summary>
    /// ��¼��NPC��һ�������ߵ���һ��������·����SO�ļ�
    /// </summary>
    public SceneRouteDataList_SO npcRoutes_SO;
    /// <summary>
    /// ��from��to�ĳ�����·����������ڲ��ҳ���·��
    /// </summary>
    private Dictionary<string, SceneRoute> sceneRouteDict = new Dictionary<string, SceneRoute>();

    protected override void Awake()
    {
        base.Awake();
        InitSceneRouteDict();
    }
    private void InitSceneRouteDict()
    {
        foreach (SceneRoute route in npcRoutes_SO.sceneRouteList)
        {
            string key = route.fromSceneName + route.toSceneName;
            if (sceneRouteDict.ContainsKey(key))
                continue;
            sceneRouteDict.Add(key, route);
        }
    }
    /// <summary>
    /// �����쳡���ƶ���·��
    /// </summary>
    /// <param name="fromSceneName"></param>
    /// <param name="toSceneName"></param>
    /// <returns></returns>
    public SceneRoute GetSceneRoute(string fromSceneName, string toSceneName)
    {
        if (sceneRouteDict.ContainsKey(fromSceneName + toSceneName))
        {
            return sceneRouteDict[fromSceneName + toSceneName];
        }
        Debug.Log("�ֵ��в�����" + fromSceneName + "��" + toSceneName);
        return null;
    }

}
