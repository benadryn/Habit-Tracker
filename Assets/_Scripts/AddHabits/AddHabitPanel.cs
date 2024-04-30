using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class AddHabitPanel : MonoBehaviour
{
    public delegate void OnNewHabitCreated();

    public static event OnNewHabitCreated OnHabitCreated;

    [SerializeField] private GameObject habitPrefab;
    [SerializeField] private GameObject habitGrid;

    private TMP_InputField _textInput;
    private HabitManager _habitManager;

    private void Start()
    {
        _textInput = GetComponentInChildren<TMP_InputField>();
        _habitManager = HabitManager.Instance;
    }

    private void Update()
    {
        if (isActiveAndEnabled && _habitManager.CheckTouchInput())
        {
            ClosePanelClick();
        }
    }

    public void AddHabitClickPanel()
    {
        if (_habitManager.Habits.All(habit => habit.Name != _textInput.text) && _habitManager.Habits.Count < 7)
        {
            var newHabit = Instantiate(habitPrefab, habitGrid.transform);
            var habitText = newHabit.GetComponentInChildren<TextMeshProUGUI>();
            habitText.text = _textInput.text;
            _habitManager.AddHabit(habitText.text);
            newHabit.name = habitText.text;
            OnHabitCreated?.Invoke();
        }

        ClosePanelClick();
    }

    public void ClosePanelClick()
    {
        _textInput.text = "";
        gameObject.SetActive(false);
    }
}