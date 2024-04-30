using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddHabitMonth : MonoBehaviour
{
    [SerializeField] private GameObject habitMonthPrefab;
    private HabitManager _habitManager;
    private Dictionary<string, GameObject> _habitMonths = new Dictionary<string, GameObject>();
    void Start()
    {
        _habitManager = HabitManager.Instance;
        AddHabitPanel.OnHabitCreated += AddHabitMonthToGrid;
        HabitManager.OnHabitDelete += DeleteMonth;
        HabitManager.OnHabitComplete += ChangeHabitMonthColor;
        EditHabit.OnHabitNameEdited += OnHabitChangeName;
    }

    private void AddHabitMonthToGrid()
    {
       _habitMonths.Add(_habitManager.Habits[transform.childCount].Name, Instantiate(habitMonthPrefab, transform));
    }

    private void ChangeHabitMonthColor(int habitIndex)
    {
        var currentDay = DateTime.Today.Day;
        var habitFinished = _habitMonths[_habitManager.Habits[habitIndex].Name].GetComponentsInChildren<Image>();

        habitFinished[currentDay].color = habitFinished[currentDay].color == Color.green ? Color.black : Color.green;
    }

    private void OnHabitChangeName(string oldName, string newName)
    {
        if (_habitMonths.ContainsKey(oldName))
        {
            var value = _habitMonths[oldName];
            _habitMonths.Remove(oldName);
            _habitMonths.Add(newName, value);
        }
    }
    
    private void DeleteMonth(string habitName)
    {
        Destroy(_habitMonths[habitName]);
        _habitMonths.Remove(habitName);
    }
}
