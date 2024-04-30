using UnityEngine;
using UnityEngine.UI;
using Toggle = UnityEngine.UI.Toggle;

public class YearHabitAdd : MonoBehaviour
{
    public static YearHabitAdd Instance;

    [SerializeField] private Scrollbar scrollbar;
    
    private float _prefabHeight = 66;
    private float _contentHeight;
    private float _currentHeight;
    private RectTransform _rectTransform;
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
        _rectTransform = gameObject.GetComponent<RectTransform>();
        
    }

    public void AddNewHabit(GameObject habitPrefab, string habitName)
    {
        GameObject newHabitGO;
        newHabitGO = Instantiate(habitPrefab, transform);
        
        var toggleText = newHabitGO.GetComponentInChildren<Toggle>().GetComponentInChildren<Text>();
        toggleText.text = habitName;
        
        _currentHeight += _prefabHeight;
        _contentHeight = _rectTransform.rect.height;
        if (_currentHeight > _contentHeight)
        {
            _rectTransform.sizeDelta = new Vector2(_rectTransform.sizeDelta.x, _currentHeight);
            scrollbar.value = 0;
        }
    }
}