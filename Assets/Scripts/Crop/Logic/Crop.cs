using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crop : MonoBehaviour
{
    public CropDetails cropDetails;

    private int harvestActionCount;

    private TileDetail tileDetail;


    public void ToolActionProcess(int toolID,TileDetail tileDetail)
    {
        this.tileDetail = tileDetail;
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
                else
                {

                }
            }

            if (tileDetail != null)
            {
                tileDetail.harvestTimes++;

                if (cropDetails.ReglowTimes > tileDetail.harvestTimes + 1)
                {
                    tileDetail.seedSinceDay -= cropDetails.dayToReglow;
                }
                else
                {
                    tileDetail.harvestTimes = -1;
                    tileDetail.seedID = -1;
                    tileDetail.seedSinceDay = -1;
                }

                EventHandler.CallUpRefleshMapDateEvent();

            }


        }

    }



}
