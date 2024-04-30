using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Button = UnityEngine.UI.Button;

public class HabitManager : MonoBehaviour
{
    public static HabitManager Instance;

    public delegate void OnHabitCompleted(int habitIndex);
    public static event OnHabitCompleted OnHabitComplete;

    public delegate void OnHabitDeleted(string habitName);

    public static event OnHabitDeleted OnHabitDelete;
    
    public Dictionary<GameObject, bool> selectedHabits = new Dictionary<GameObject, bool>();
    private List<GameObject> keysToRemove = new List<GameObject>();
    
    public List<Habit> Habits = new List<Habit>();
    
    [SerializeField] private Button deleteHabitButton;
    [SerializeField] private Button editHabitButton;
    private bool _isClickable;
    private GameObject _habitToDelete;
    private bool _isChecked;
    private Camera _camera;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        _camera = Camera.main;
        EditHabit.OnHabitNameEdited += OnHabitChangeName;
    }

    private void Update()
    {
        deleteHabitButton.interactable = _isClickable;
        
    }
    public void CheckSelected(GameObject obj)
    {
            if (selectedHabits.ContainsKey(obj))
            {
                RemoveSelectedHabit(obj);
            }
            else
            {
                AddSelectedHabit(obj);
            }
    }

    private void AddSelectedHabit(GameObject obj)
    {
        _isClickable = true;
        selectedHabits[obj] = true;
        obj.GetComponent<Image>().color = Color.green;
        EditBtnClickable();
    }

    public void RemoveSelectedHabit(GameObject obj)
    {
        if (selectedHabits.Count == 0)
        {
            _isClickable = false;
        }
        obj.GetComponent<Image>().color = Color.black;
        selectedHabits.Remove(obj);
        EditBtnClickable();
    }

    public void DeleteSelectedHabit()
    {
        foreach (var (key, _) in selectedHabits)
        {
            OnHabitDelete?.Invoke(key.name);
            keysToRemove.Add(key);
        }

        foreach (var key in keysToRemove)
        {
            selectedHabits.Remove(key);
            var habitToRemove = Habits.FirstOrDefault(habit => habit.Name == key.name);
            Habits.Remove(habitToRemove);
            Destroy(key);
        }
        keysToRemove.Clear();
        _isClickable = false;
        EditBtnClickable();
    }

    public void AddHabit(string habitName)
    {
        Habit newHabit = new Habit();
        newHabit.Name = habitName;
        newHabit.CompletionStatus = new bool[7];
        Habits.Add(newHabit);
    }

    public void ToggleCompletionStatus(int habitIndex)
    {
        var dayIndex = GetDayIndex();
        if (habitIndex >= 0 && habitIndex < Habits.Count && dayIndex is >= 0 and < 7)
        {
            Habits[habitIndex].CompletionStatus[dayIndex] = !Habits[habitIndex].CompletionStatus[dayIndex];
            OnHabitComplete?.Invoke(habitIndex);
        }
    }

    public int GetDayIndex()
    {
        DayOfWeek currentDay = DateTime.Now.DayOfWeek;
        return (int)currentDay;
    }

    private void EditBtnClickable()
    {
        editHabitButton.interactable = selectedHabits.Count == 1;
    }

    public bool CheckTouchInput()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began || touch.phase == TouchPhase.Moved)
            {
                var touchPos = _camera.ScreenToWorldPoint(touch.position);

                var hit = Physics2D.Raycast(touchPos, Vector2.zero);

                if (!hit)
                {
                    return true;
                }
            }
        }

        return false;
    }
    
    private void OnHabitChangeName(string oldName, string newName)
    {
        foreach (var habit in Habits)
        {
            if (habit.Name == oldName)
            {
                habit.Name = newName;
                return;
            }
        }
    }
}