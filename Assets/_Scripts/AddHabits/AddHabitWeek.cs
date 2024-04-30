using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddHabitWeek : MonoBehaviour
{
    private HabitManager _habitManager;
    [SerializeField] private GameObject habitWeekPrefab;
    private Dictionary<string, GameObject> _habitWeeks = new Dictionary<string, GameObject>();

    void Start()
    {
        _habitManager = HabitManager.Instance;
        AddHabitPanel.OnHabitCreated += AddHabitWeekToGrid;
        HabitManager.OnHabitComplete += ChangeHabitWeekColor;
        HabitManager.OnHabitDelete += DeleteWeek;
        EditHabit.OnHabitNameEdited += OnHabitChangeName;
    }

    private void AddHabitWeekToGrid()
    {
        _habitWeeks.Add(_habitManager.Habits[transform.childCount].Name, Instantiate(habitWeekPrefab, transform));
    }

    private void ChangeHabitWeekColor(int habitIndex)
    {
        var dayIndex = _habitManager.GetDayIndex();
        var habitFinished = _habitWeeks[_habitManager.Habits[habitIndex].Name].GetComponentsInChildren<Image>();
        

        habitFinished[dayIndex].color = habitFinished[dayIndex].color == Color.green ? Color.black : Color.green;
    }
    private void OnHabitChangeName(string oldName, string newName)
    {
        if (_habitWeeks.ContainsKey(oldName))
        {
            var value = _habitWeeks[oldName];
            _habitWeeks.Remove(oldName);
            _habitWeeks.Add(newName, value);
        }
    }
    private void DeleteWeek(string habitName)
    {
        Destroy(_habitWeeks[habitName]);
        _habitWeeks.Remove(habitName);
    }
}