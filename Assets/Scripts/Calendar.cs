using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Calendar : LuckObject
{
    [SerializeField] private DayOfWeek m_dayOfWeek;
    [SerializeField] private int m_day;
    [SerializeField] private Text dayOfWeekText;
    [SerializeField] private Text dayText;

    public DayOfWeek dayOfWeek
    {
        get { return m_dayOfWeek; }
        set
        {
            m_dayOfWeek = value;
            dayOfWeekText.text = value.ToString();
        }
    }

    public int day
    {
        get { return m_day; }
        set
        {
            m_day = value;
            dayText.text = value.ToString();
        }
    }

    void Awake()
    {
        dayOfWeek = dayOfWeek;
        day = day;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Dice"))
        {
            day++;
            dayOfWeek = (DayOfWeek)(((int)dayOfWeek + 1) % 7);
            if (dayOfWeek == DayOfWeek.Friday && day == 13)
            {
                AlterLuck();
                AudioManager.instance.Play("Umbrella");
            }
        }
    }
}
