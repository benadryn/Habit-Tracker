using UnityEngine;
using UnityEngine.EventSystems;

public class AddHabitBtn : MonoBehaviour
{
    [SerializeField] private GameObject addHabitPanel;

    public void AddHabitClick()
    {
        // EventSystem.current.SetSelectedGameObject(null);
        addHabitPanel.SetActive(true);
    }
}
