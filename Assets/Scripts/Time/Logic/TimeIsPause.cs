using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeIsPause : MonoBehaviour
{
    private void OnEnable()
    {
        EventHandler.timeControlEvent += timeIsPause;
    }

    /// <summary>
    /// ���ͨ������timeControlEvent������timeControlEvent�¼�֮�����timeIsPause
    /// </summary>
    /// <param name="isPause">isPause����TimeManager.gameClockPause</param>
    public void timeIsPause(bool isPause)
    {
        TimeManager.gameClockPause = !isPause;
    }

    // ͨ��UI��Button�������������ʱ��
    public void timeControlEvent()
    {
        EventHandler.CallUpTimeControlEvent(TimeManager.gameClockPause);
    }
}
