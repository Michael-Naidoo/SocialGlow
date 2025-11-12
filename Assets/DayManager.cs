// DayManager.cs (Attach to the GameManager GameObject)

using System.Collections.Generic;
using UnityEngine;

public class DayManager : MonoBehaviour
{
    [SerializeField] private List<WorkDayData> dayCycleData;
    private WorkDayData currentDayData;

    private void Start()
    {
        // For prototype simplicity, we'll start with Day 1 data
        SetDayData(1); 
    }

    public void SetDayData(int dayNumber)
    {
        currentDayData = dayCycleData.Find(d => d.dayNumber == dayNumber);

        if (currentDayData == null)
        {
            Debug.LogError($"WorkDayData for Day {dayNumber} not found!");
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