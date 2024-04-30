using System.Linq;
using TMPro;
using UnityEngine;

public class EditHabit : MonoBehaviour
{
    public delegate void OnHabitNameEdit(string oldName, string newName);

    public static event OnHabitNameEdit OnHabitNameEdited; 
    
    private TMP_InputField _textInput;
    private HabitManager _habitManager;
    private GameObject _selectedHabit;


    void Start()
    {
        _textInput = GetComponentInChildren<TMP_InputField>();
        _habitManager = HabitManager.Instance;
    }

    void Update()
    {
        if (isActiveAndEnabled && _habitManager.CheckTouchInput())
        {
            ClosePanel();
        }
    }

    public void EditNameOfHabit()
    {
        foreach (var habitPair in _habitManager.selectedHabits)
        {
            _selectedHabit = habitPair.Key;
        }
        
        if (!_selectedHabit) return;
        var habitText = _selectedHabit.GetComponentInChildren<TextMeshProUGUI>();
        OnHabitNameEdited?.Invoke(habitText.text, _textInput.text);
        habitText.text = _textInput.text;
        _selectedHabit.name = _textInput.text;
    }

    private void ClosePanel()
    {
        _textInput.text = "";
        gameObject.SetActive(false);
    }
}