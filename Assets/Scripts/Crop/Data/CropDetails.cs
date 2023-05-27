using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CropDetails
{
    [Header("������Ϣ")]
    public int seedID;
    public Season[] season;
    public int[] growthDays;
    public int TotalGlowthDays { 
        get 
        {
            int amount = 0;
            foreach (int day in growthDays)
            {
                amount += day;
            }
            return amount;
        } }
    public GameObject[] seedPrefabs;
    public Sprite[] seedSprite;



    [Space]
    [Header("�ո��")]
    public int[] harvestToolID;
    public int[] harvestActionCount;
    public int TransferNewItemID;



    [Space]
    [Header("�ջ��ʵ��Ϣ")]
    public int[] productedItemID;
    public int[] MinAmount;
    public int[] MaxAmount;
    public int spawnRadius;

    [Space]
    [Header("�ٴ�����ʱ��")]
    public int dayToReglow;
    public int ReglowTimes;

    [Header("����ѡ��")]
    public bool GenarateAtPlayerHead;
    public bool haveAnimation;
    public bool particalEffect;
    public E_PESType[] ParticalEffectSystem;


    public bool CheckToolValid(int toolID)
    {
        foreach (int i in harvestToolID)
        {
            if (i == toolID)
            {
                return true;
            }
        }
        return false;
    }

}
