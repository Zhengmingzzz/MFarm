using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public enum ItemType
{
    Seed,Commodity,Furniture,
    //��ͷ  ��������  ��ʯͷ���� ��ݹ��� ��ˮ     �ո��
    HoeTool,ChopTool,BreakTool,ReapTool,WaterTool,CollectionTool,
    //�ɱ������Ʒ���Ӳݣ�
    ReapableScenery,Trunk

}

public enum SlotType
{
    Bag,Shop,Box
}



public enum InventoryLocation
{
    Player,Box,Shop
}

public enum BodyTypeName
{
    Arm,Body,Hair,Tool
}

public enum NowState
{
    Carry,None,Hoe,Water,Harvest,Axe,PickAxe,ReapTool
}

public enum Season
{
    ����,����,����,����
}
/// <summary>
/// �������� �Ƿ���ھ� �ɶ�����...
/// </summary>
public enum E_GridType
{
    CanDig,CanDrop, CanPlaceFurniture,NPC_Obstacle,
}

public enum E_PESType
{
    None,LeaveFalling01,LeaveFalling02,Rock,ReapItem
}