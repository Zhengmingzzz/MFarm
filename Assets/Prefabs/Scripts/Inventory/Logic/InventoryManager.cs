using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�뱳�� �����йصĶ����õ�һ�������ռ�
namespace MFarm.Inventory
{
    public class InventoryManager : Singleton<InventoryManager>
    {
        [Header("������Ʒ����")]
        public ItemDetailList_SO itemDetailList;

        [Header("��������")]
        public InventoryBag_SO playerBag;

        private void Start()
        {
            EventHandler.CallUpdataInventoryUI(InventoryLocation.Player, playerBag.itemList);
        }
        public ItemDetails GetItemDetails(int Index)
        {
            foreach (ItemDetails t in itemDetailList.ItemDetailsList)
            {
                if (t.ItemID == Index)
                {
                    return t;
                }
            }
            return null;
        }

        public int bagIsFull()
        {
            int count = 0;
            foreach (InventoryType i in playerBag.itemList)
            {
                if (i.ItemAmount == 0)
                {
                    return count;
                }
                count++;
            }
            return -1;
        }

        
        public void PickedUpItem(RenderItem item , bool toDestroy)
        {
            int isExistItem()
            {
                int count = 0;
                foreach (InventoryType i in playerBag.itemList)
                {
                    if (item.ItemID == i.ItemID)
                    {
                        return count;
                    }
                    count++;
                }
                return -1;
            }

            int Exist_Index = isExistItem();
            int NULL_Index = bagIsFull();



            InventoryType newBagPos;

            if (Exist_Index != -1)
            {
                newBagPos.ItemID = item.ItemID;
                newBagPos.ItemAmount = playerBag.itemList[Exist_Index].ItemAmount + 1;
                playerBag.itemList[Exist_Index] = newBagPos;

            }
            else if (NULL_Index == -1)
            {
                return;
            }
            else if(NULL_Index != -1)
            {
                newBagPos.ItemID = item.ItemID;
                newBagPos.ItemAmount = 1;
                playerBag.itemList[NULL_Index] = newBagPos;
            }
            

                //������ݵ�����
            
            if (toDestroy == true)
            {
                Destroy(item.gameObject);
            }


            //����UI
            EventHandler.CallUpdataInventoryUI(InventoryLocation.Player, playerBag.itemList);
        }
        public void SortBag()
        {
            InventoryType t;

            for (int i = playerBag.itemList.Count - 1; i >= 0; i--)
            {
                
                for (int j = 0; j < i - 1; j++)
                {
                    if (playerBag.itemList[j + 1].ItemID > playerBag.itemList[j].ItemID)
                    {
                        t = playerBag.itemList[j + 1];
                        playerBag.itemList[j + 1] = playerBag.itemList[j];
                        playerBag.itemList[j] = t;
                    }
                }
            }
        }

        public void SwapItem(int from,int to)
        {
            InventoryType current = playerBag.itemList[from];
            InventoryType target = playerBag.itemList[to];

            if (playerBag.itemList[to].ItemAmount != 0)
            {
                playerBag.itemList[from] = target;
                playerBag.itemList[to] = current;
            }
            else
            {
                playerBag.itemList[from] = new InventoryType();
                playerBag.itemList[to] = current;
            }
            EventHandler.CallUpdataInventoryUI(InventoryLocation.Player, playerBag.itemList);
        }


    }


    
}


