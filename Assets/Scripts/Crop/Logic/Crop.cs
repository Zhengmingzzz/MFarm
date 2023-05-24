using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crop : MonoBehaviour
{
    public CropDetails cropDetails;

    private int harvestActionCount;




    public void ToolActionProcess(int toolID)
    {
        int requirActionCount = GetRequirCount(toolID);
        if (requirActionCount == -1) return;


        if (harvestActionCount < requirActionCount)
        {
            harvestActionCount++;

            //TODO:���Ŷ���
            //TODO:��������Ч��
        }
        if (harvestActionCount >= requirActionCount)
        {
            SpawnCrop();


        }
    }

    private int GetRequirCount(int toolID)
    {
        for (int i = 0; i<cropDetails.harvestToolID.Length;i++)
        {
            if (cropDetails.harvestToolID[i] == toolID)
            {
                return cropDetails.harvestActionCount[i];
            }
        }
        return -1;
    }


    private void SpawnCrop()
    {
        for (int i = 0; i < cropDetails.productedItemID.Length; i++)
        {
            int amount;
            if (cropDetails.MinAmount[i] == cropDetails.MaxAmount[i])
            {
                amount = cropDetails.MinAmount[i];
            }
            else
            {
                amount = Random.Range(cropDetails.MinAmount[i], cropDetails.MaxAmount[i] + 1);
            }


            for (int j = 0; j < amount; j++)
            {
                //�������ͷ������    
                //1 ������Ʒ�ڱ�����
                //2 ������Ʒ�ڽ�ɫͷ��
                if (cropDetails.GenarateAtPlayerHead)
                {
                    EventHandler.CallUpHarvestCropOnPlayer(cropDetails.productedItemID[i]);
                }
                //����
                //������Ʒ�ڵ�ͼ��
            }
        }

    }



}
