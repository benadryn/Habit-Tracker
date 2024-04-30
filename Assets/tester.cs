using TMPro;
using UnityEngine;

public class tester : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;

    [SerializeField] private TMP_InputField inputField;

    public void OnClick()
    {
        text.text = inputField.text;
    }

    
}
