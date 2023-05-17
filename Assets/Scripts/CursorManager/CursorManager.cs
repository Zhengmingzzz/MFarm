using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

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

    private bool isTransition;



    private void Start()
    {
        Cursor.visible = false;

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
            CursorImage.transform.position = Input.mousePosition;
            if (isInterActWithUI())
            {
                SetCursorImage(UISprite);
            }
            else
            {
                SetCursorImage(currentSprite);
                if (!isTransition)
                    CheckCursorValid();
                else
                {
                    SetCursorValidColor(true);
                    currentSprite = normal;
                    SetCursorImage(currentSprite);
                }

            }
        }

        if (Cursor.visible != false && Application.isPlaying)
        {
            Cursor.visible = false;
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
        EventHandler.BeforeUnLoadSceneEvent += OnBeforeUnLoadSceneEvent;
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
    }



    private void OnAfterLoadSceneEvent()
    {
        isTransition = false;
        currentGrid = FindObjectOfType<Grid>();
    }




    private void CheckCursorValid()
    {
        mouseWorldPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);

        mouseGridPos = currentGrid.WorldToCell(mouseWorldPos);

        if (isSelected)
        {
            GridDetail CheckGridDetailInfo = MFarm.Map.GridMapManager.Instance.getGridDetailByPos(mouseGridPos);

            if (selectedItemDetail != null && CheckGridDetailInfo != null)
            {
                switch (selectedItemDetail.itemType)
                {
                    case ItemType.Commodity:
                        if (CheckGridDetailInfo.CanDropItem == true)
                        {
                            SetCursorValidColor(true);
                        }
                        else
                        {
                            SetCursorValidColor(false);
                        }
                        break;
                    case ItemType.Furniture:
                        if (CheckGridDetailInfo.CanPlaceFurniture == true)
                        {
                            SetCursorValidColor(true);
                        }

                        else
                        {
                            SetCursorValidColor(false);
                        }
                        break;
                    case ItemType.Seed:
                        if (CheckGridDetailInfo.CanDig == true)
                        {
                            SetCursorValidColor(true);
                        }
                        else
                        {
                            SetCursorValidColor(false);
                        }
                        break;
                }

            }
            else
            {
                SetCursorValidColor(false);
            }
        }
        else
        {
            currentSprite = normal;
            SetCursorImage(currentSprite);
            SetCursorValidColor(true);
        }
        


    }

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
}
