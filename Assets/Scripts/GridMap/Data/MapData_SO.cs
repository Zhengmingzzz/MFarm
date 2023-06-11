using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MapData_SO",menuName = "Map/MapData_SO")]
public class MapData_SO : ScriptableObject
{
    [SceneName]public string SceneName;
    [Header("��ͼ��Ϣ")]
    public int gridHeight;
    public int gridWidth;
    [Header("�����½���������")]
    public int originX;
    public int originY;
    [Space(1)]

    public List<TileProperty> TilePropertiesList = new List<TileProperty>();
}
