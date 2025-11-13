// DayManager.cs (Attach to the GameManager GameObject)

using System;
using System.Collections.Generic;
using UnityEngine;

public class DayManager : MonoBehaviour
{
    public static DayManager Instance { get; private set; }
    [SerializeField] private List<WorkDayData> dayCycleData;
    private WorkDayData currentDayData;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            // Keep the GameManager across scene loads
        }
    }

    private void Start()
    {
        // For prototype simplicity, we'll start with Day 1 data
        SetDayData();
    }

    public void SetDayData()
    {
        currentDayData = dayCycleData.Find(d => d.dayNumber == GameManager.Instance.currentDay);

        if (currentDayData == null)
        {
            Debug.LogError($"WorkDayData for Day {currentDayData} not found!");
        }
        else
        {
            Debug.Log($"Loaded data for Day {currentDayData.dailyTheme}");
        }
    }

    public WorkDayData GetCurrentDayData()
    {
        return currentDayData;
    }
}