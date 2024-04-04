using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using MFarm.CropPlant;
using UnityEngine.SceneManagement;
public class CursorManager : MonoBehaviour
{
    public Sprite normal, tool, cursorSeed, goods, UISprite, Furniture;
    private RectTransform cursorCanvasTransfrom;
    private Image CursorImage;
    private Sprite currentSprite;

    private Camera mainCamera;
    private Grid currentGrid;

    private Vector3 mouseWorldPos;
    private Vector3Int mouseGridPos;

    private ItemDetails selectedItemDetail;
    private bool isSelected;
    /// <summary>
    /// �ж��Ƿ������л�����
    /// </summary>
    private bool isTransition;
    private bool mouseValid;

    private Transform playerTransform;

    private bool RadiumValid;


    private void Awake()
    {
        playerTransform = FindObjectOfType<Player>().transform;

    }

    private void Start()
    {
        cursorCanvasTransfrom = GameObject.FindGameObjectWithTag("CursorCanvas").GetComponent<RectTransform>();
        CursorImage = cursorCanvasTransfrom.GetChild(0).GetComponent<Image>();

        SetCursorImage(normal);
        currentSprite = normal;

        mainCamera = GameObject.FindObjectOfType<Camera>();
    }

    private void Update()
    {
        if (CursorImage != null)
        {
            // ����UIͼ��������λ��
            CursorImage.transform.position = Input.mousePosition;
            // �������ϷUI�����������UIͼ��
            if (isInterActWithUI())
            {
                SetCursorValidColor(true);
                SetCursorImage(UISprite);
            }
            else
            {
                SetCursorImage(currentSprite);
                if (!isTransition)
                {
                    CheckCursorValid();
                }
                else
                {
                    SetCursorValidColor(true);
                    currentSprite = normal;
                    selectedItemDetail = null;
                    SetCursorImage(currentSprite);
                    isSelected = false;
                }
            }
            OnmouseClicked();
        }


    }

    private void OnEnable()
    {
        EventHandler.ItemSelectEvent += OnItemSelectEvent;
        EventHandler.BeforeUnLoadSceneEvent += OnBeforeUnLoadSceneEvent;
        EventHandler.AfterLoadSceneEvent += OnAfterLoadSceneEvent;


    }

    private void OnDisable()
    {
        EventHandler.ItemSelectEvent -= OnItemSelectEvent;
        EventHandler.BeforeUnLoadSceneEvent -= OnBeforeUnLoadSceneEvent;
        EventHandler.AfterLoadSceneEvent -= OnAfterLoadSceneEvent;

    }



    private void OnItemSelectEvent(ItemDetails itemDetail, bool isSelected)
    {
        this.isSelected = isSelected;

        
        if (!isSelected)
        {
            selectedItemDetail = null;
            currentSprite = normal;            
        }
        else
        {
            selectedItemDetail = itemDetail;
            currentSprite = itemDetail.itemType switch 
            {
                ItemType.Seed =>cursorSeed,
                ItemType.Commodity=>goods,
                ItemType.Furniture => Furniture,
                ItemType.HoeTool=>tool,
                ItemType.BreakTool => tool,
                ItemType.ChopTool => tool,
                ItemType.CollectionTool => tool,
                ItemType.ReapTool => tool,
                ItemType.WaterTool => tool,
                _=>normal,
            };
        }

    }

    /// <summary>
    /// �ж��Ƿ����ϷUI����
    /// </summary>
    /// <returns></returns>
    private bool isInterActWithUI()
    {
        if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void SetCursorImage(Sprite sprite)
    {
        if (CursorImage != null)
        {
            CursorImage.sprite = sprite;
        }
    }

    private void OnBeforeUnLoadSceneEvent()
    {
        isTransition = true;
        isSelected = false;
        selectedItemDetail = null;
    }



    private void OnAfterLoadSceneEvent()
    {
        currentGrid = FindObjectOfType<Grid>();
        isTransition = false;
    }




    private void CheckCursorValid()
    {
        // �����ָ����Χ�ڣ�������Ļ��Χ��û����꣬�����UI����
        if (!new Rect(0, 0, Screen.width, Screen.height).Contains(Input.mousePosition))
        {
            Cursor.visible = true;
        }
        else
        {
            Cursor.visible = false;

        }

        // �������λ�õõ���Ӧ����������(Vec3 float����) �ٸ�����������תΪ��������(Vec3Int����)
        mouseWorldPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mouseGridPos = currentGrid.WorldToCell(mouseWorldPos);

        var playerGridPos = currentGrid.WorldToCell(playerTransform.position);


        // ���û���л���������ѡ������Ʒ������� ��Ҫ�����UI��������
        if (!isTransition && isSelected)
        {
            // �洢����������������µ�tile��Ϣ
            TileDetail CheckTileDetailInfo = null;
            // �洢����������������µ����ӵ���Ϣ
            Crop crop = MFarm.Map.GridMapManager.Instance.FindCropByMouseWorldPos(mouseWorldPos);
            
            // ����ϣ�����������������ʹ���Ϸ��������Ҫ������ũ��������
            if (selectedItemDetail.itemType == ItemType.ChopTool)
            {
                if (crop != null)
                {
                    // ��Ϊ���Ƚϴ�������Ҫ�������ĸ����������
                    CheckTileDetailInfo = MFarm.Map.GridMapManager.Instance.getTileDetailByPos(SceneManager.GetActiveScene().name,new Vector3Int(crop.tileDetail.gridX, crop.tileDetail.gridY, 0));
                }
            }
            else
            {
                CheckTileDetailInfo = MFarm.Map.GridMapManager.Instance.getTileDetailByPos(SceneManager.GetActiveScene().name, mouseGridPos);
            }


            // ���ˣ�ѡ��Ĺ�����Ϣ ����Ӧ��tile��Ϣ ��Ӧ��crop��Ϣ�Ļ�ȡ �Ѿ����
            // ���������ѡ�񹤾߷�Χ�����ж�
            if (selectedItemDetail != null && CheckTileDetailInfo != null)
            {
                RadiumValid = CheckUseRadiusValid(selectedItemDetail, new Vector3Int(CheckTileDetailInfo.gridX, CheckTileDetailInfo.gridY, 0));

                mouseValid = false;
                CropDetails cropDetails = CropManager.Instance.GetCropDetailsByID(CheckTileDetailInfo.seedID);


                if (!RadiumValid)
                {
                    mouseValid = false;
                    goto PassSwitch;

                }

                //TODO:����µĹ���
                switch (selectedItemDetail.itemType)
                {
                    case ItemType.Commodity:
                        if (CheckTileDetailInfo.CanDropItem == true)
                        {
                            mouseValid = true ;
                        }
                        break;
                    case ItemType.Furniture:
                        if (CheckTileDetailInfo.CanPlaceFurniture == true)
                        {
                            mouseValid = true;
                        }
                        break;
                    case ItemType.Seed:
                        if (CheckTileDetailInfo.digSinceDay != -1 && CheckTileDetailInfo.seedID == -1)
                        {
                            mouseValid = true;
                            SetCursorValidColor(true);
                        }
                        else
                        {
                            SetCursorValidColor(false);
                        }
                        break;

                    case ItemType.HoeTool:
                        if (CheckTileDetailInfo.CanDig)
                        {
                            mouseValid = true;
                        }
                        break;
                    case ItemType.WaterTool:
                        if (CheckTileDetailInfo.digSinceDay != -1 && CheckTileDetailInfo.wateredSinceDay == -1)
                        {
                            mouseValid = true;
                        }
                        break;
                    case ItemType.ChopTool:
                    case ItemType.BreakTool:
                        if (crop != null && crop.cropDetails.CheckToolValid(selectedItemDetail.ItemID) && crop.canHarvest)
                        {
                            mouseValid = true;
                        }
                        break;
                    case ItemType.CollectionTool:
                        if (cropDetails != null && cropDetails.CheckToolValid(selectedItemDetail.ItemID)) 
                        {
                            if (cropDetails.TotalGlowthDays <= CheckTileDetailInfo.seedSinceDay)
                            {
                                mouseValid = true;
                            }
                        }
                        break;
                    case ItemType.ReapTool:
                        if (MFarm.Map.GridMapManager.Instance.CheckReapItemValidInRadium(selectedItemDetail, mouseWorldPos))
                        {
                            mouseValid = true;
                        }
                        break;
                }
            PassSwitch:
                SetCursorValidColor(mouseValid);

            }
            else
            {
                mouseValid = false;
                SetCursorValidColor(mouseValid);
            }

        }
        else
        {
            currentSprite = normal;
            SetCursorImage(currentSprite);
            SetCursorValidColor(true);
            mouseValid = true;

        }
        


    }
    /// <summary>
    /// ���ݴ�������������UI��ɫ
    /// </summary>
    /// <param name="isValid"></param>
    private void SetCursorValidColor(bool isValid)
    {
        if (isValid == true)
        {
            CursorImage.color = new Color(1, 1, 1, 1);
        }
        else
        {
            CursorImage.color = new Color(1, 0, 0, 0.4f);
        }
    }

    private void OnmouseClicked()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (selectedItemDetail != null && !isTransition && mouseValid)
            {
                EventHandler.CallUpMouseClickedEvent(mouseWorldPos, selectedItemDetail);
            }
        }
            
    }

    /// <summary>
    /// ����itemDetail�ķ�Χ��playerTransform���������λ�ã��жϺϷ�����
    /// </summary>
    /// <param name="itemDetail"></param>
    /// <param name="TargetItemPos"></param>
    /// <returns></returns>
    private bool CheckUseRadiusValid(ItemDetails itemDetail,Vector3Int TargetItemPos)
    {
        var playerGridPos = currentGrid.WorldToCell(playerTransform.position);
        if (Vector3Int.Distance(playerGridPos ,TargetItemPos) > itemDetail.itemUseRadius) 
        {
            return false;
        }
        return true;
    }


}
