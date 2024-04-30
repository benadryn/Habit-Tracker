using UnityEngine;
using UnityEngine.UI;

public class AddToggleToDay : MonoBehaviour
{
    public static AddToggleToDay Instance;
    
    [SerializeField] private Toggle toggleDayPrefab;
    private GameObject[] objectsWithContentTag;
    
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
        objectsWithContentTag = GameObject.FindGameObjectsWithTag("Content");
    }

    public void AddHabitToDay(string habitName)
    {
        foreach (var obj in objectsWithContentTag)
        {
            var newToggleObj = Instantiate(toggleDayPrefab, obj.transform);
            var toggleText = newToggleObj.GetComponentInChildren<Toggle>().GetComponentInChildren<Text>();
            toggleText.text = habitName;
        }   
    }
}
