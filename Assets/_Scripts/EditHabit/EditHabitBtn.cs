using UnityEngine;

public class EditHabitBtn : MonoBehaviour
{
   [SerializeField] private GameObject editHabitPanel;

   public void OnEditClick()
   {
      editHabitPanel.gameObject.SetActive(true);
   }
}
