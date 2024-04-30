using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddHabitToggle : MonoBehaviour
{
    private HabitManager _habitManager;
    [SerializeField] private GameObject habitTogglePrefab;
    private Dictionary<string, GameObject> _habitToggles = new Dictionary<string, GameObject>();

    void Start()
    {
        _habitManager = HabitManager.Instance;
        AddHabitPanel.OnHabitCreated += AddHabitToggleToGrid;
        HabitManager.OnHabitDelete += DeleteToggle;
        EditHabit.OnHabitNameEdited += OnHabitChangeName;
    }

    private void AddHabitToggleToGrid()
    {
        var newToggle = Instantiate(habitTogglePrefab, transform);
        var toggle = newToggle.GetComponentInChildren<Toggle>();
        var toggleIndex = transform.childCount - 1;
        toggle.onValueChanged.AddListener(delegate { OnCompletedHabit(toggleIndex); });
        _habitToggles.Add(_habitManager.Habits[transform.childCount - 1].Name, newToggle);
    }

    public void OnCompletedHabit(int toggleIndex)
    {
        _habitManager.ToggleCompletionStatus(toggleIndex);
    }
    private void OnHabitChangeName(string oldName, string newName)
    {
        if (_habitToggles.ContainsKey(oldName))
        {
            var value = _habitToggles[oldName];
            _habitToggles.Remove(oldName);
            _habitToggles.Add(newName, value);
        }
    }
    private void DeleteToggle(string habitName)
    {
        Destroy(_habitToggles[habitName]);
        _habitToggles.Remove(habitName);
    }
}