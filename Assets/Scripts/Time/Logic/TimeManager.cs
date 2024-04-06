using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// ����������Ϸ��ʱ��ϵͳ����ʵ��һ���൱����Ϸ�е�45��
/// </summary>
public class TimeManager : Singleton<TimeManager>
{
    private int second, minute, hour, day, mouth, year;
    private Season gameSeason = Season.����;
    private int seasonInMouth = 3;

    private float tickTime = 0f;

    // �����������������Ϸʱ�������
    public static bool gameClockPause = false;
    public static bool isAccelerate = false;

    public TimeSpan GameTimeSpan => new TimeSpan(hour, minute, second);

    /// <summary>
    /// ��Ϸ��һ��ʼ��ʱ��
    /// </summary>
    private void newGameTime()
    {
        second = 0;
        minute = 0;
        hour = 8;
        day = 1;
        mouth = 1;
        year = 2022;
        gameSeason = Season.����;
    }

    protected override void Awake()
    {
        base.Awake();
        newGameTime();
    }

    private void Start()
    {
        EventHandler.CallUpUpdataDate(year, mouth, day, gameSeason);
        EventHandler.CallUpUpdataTime(minute, hour, day, gameSeason);
    }

    private void Update()
    {
        timeControl();
    }

    private void FixedUpdate()
    {

        if (!gameClockPause)
        {
            float timeThreshold = Settings.secondThreshold;
            tickTime += Time.deltaTime;

            if (tickTime > timeThreshold)
                UpdataTime();
        }
    }

    /// <summary>
    /// ʱ����ƺ���
    /// G--��������
    /// P--��ͣʱ��
    /// H--ʱ�����
    /// </summary>
    private void timeControl()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            day++;
            EventHandler.CallUpUpdataDate(year, mouth, day, gameSeason);
            EventHandler.CallUpUpdataTime(minute, hour, day, gameSeason);
            EventHandler.CallUpUpdataGameDayEvent(day, gameSeason);
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            EventHandler.CallUpTimeControlEvent(gameClockPause);
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            isAccelerate = !isAccelerate;
        }
    }


    public void UpdataTime()
    {
        if (isAccelerate)
            second += 10;
        else
            second++;
        if (second > Settings.secondHold)
        {
            second = 0;
            minute++;

            if (minute > Settings.minuteHold)
            {
                minute = 0;
                hour++;


                if (hour > Settings.hourHold)
                {
                    hour = 0;
                    day++;
                    if (day > Settings.dayHold)
                    {
                        day = 1;
                        mouth++;
                        if (mouth > Settings.mouthHold)
                        {
                            mouth = 1;
                            seasonInMouth--;
                            if (seasonInMouth == 0)
                            {
                                seasonInMouth = 3;
                                int seasonIndex = (int)gameSeason;
                                seasonIndex++;
                                if (seasonInMouth > Settings.seasonHold)
                                {
                                    seasonIndex = 0;
                                    year++;
                                }
                                gameSeason = (Season)seasonIndex;
                            }
                        }
                    }
                    // ����/���ڸ���
                    EventHandler.CallUpUpdataGameDayEvent(day, gameSeason);
                }
                //Сʱ����
                EventHandler.CallUpUpdataDate(year, mouth, day, gameSeason);

            }

        }
        //���Ӹ���
        EventHandler.CallUpUpdataTime(minute, hour, day, gameSeason);
    }
}
