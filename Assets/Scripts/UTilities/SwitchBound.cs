using UnityEngine;
using Cinemachine;

public class SwitchBound : MonoBehaviour
{

    private void OnEnable()
    {
        EventHandler.AfterLoadSceneEvent += SwitchConfinerShape;
    }
    private void OnDisable()
    {
        EventHandler.AfterLoadSceneEvent -= SwitchConfinerShape;

    }

    public void SwitchConfinerShape()
    {
        
        PolygonCollider2D confinerShape = GameObject.FindGameObjectWithTag("BoundsConfiner").GetComponent<PolygonCollider2D>();
        CinemachineConfiner thisCinemachineConfiner = this.GetComponent<CinemachineConfiner>();
        if (confinerShape != null)
        {
            thisCinemachineConfiner.m_BoundingShape2D = confinerShape;
        }
        //�л�����ʱ����ϴα߽绺��
        thisCinemachineConfiner.InvalidatePathCache();
    }
}
