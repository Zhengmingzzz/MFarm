using UnityEngine.UI;
using TMPro;
using UnityEngine;


namespace MFarm.Inventory
{
    public class ItemToolTip : MonoBehaviour
    {
        public int UpNum;
        [SerializeField] TextMeshProUGUI ItemName;
        [SerializeField] TextMeshProUGUI M_ItemType;
        [SerializeField] TextMeshProUGUI ItemDescription;
        [SerializeField] Text ItemPrice;


        public void SetupToolTip(ItemDetails itemDetail, SlotType slotType)
        {
            ItemName.text = itemDetail.ItemName;
            M_ItemType.text = GetItemType(itemDetail.itemType);
            ItemDescription.text = itemDetail.itemDescription;

            if (itemDetail.itemType == global::ItemType.Seed || itemDetail.itemType == global::ItemType.Commodity || itemDetail.itemType == global::ItemType.Furniture)
            {
                float price = itemDetail.itemPrice;
                if (slotType == SlotType.Shop)
                {
                    price *= itemDetail.sellPercentage;
                }

                ItemPrice.text = price.ToString();
            }
            else
            {
                ItemPrice.text = "�޷�����:)";
            }
        }



        private string GetItemType(ItemType itemType)
        {
            return itemType switch
            {
                ItemType.Seed => "����",
                ItemType.Commodity => "��Ʒ",
                ItemType.Furniture => "�Ҿ�",
                ItemType.BreakTool => "����",
                ItemType.ChopTool => "����",
                ItemType.CollectionTool => "����",
                ItemType.HoeTool => "����",
                ItemType.ReapTool => "����",
                ItemType.WaterTool => "����",
                _=>"��"
            };
        }
    }
}