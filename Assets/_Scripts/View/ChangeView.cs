using UnityEngine;

public class ChangeView : MonoBehaviour
{
   [SerializeField] private GameObject[] grids;
   private int _currentGridIndex = 0;

   private void Start() 
   {
       foreach (var obj in grids)
       {
           obj.SetActive(false);
           if (grids[0] == obj)
           {
               obj.SetActive(true);
           }
       }
   }

   public void OnRightBtnClick()
   {
         grids[_currentGridIndex].gameObject.SetActive(false);
         _currentGridIndex++;
         if (_currentGridIndex > grids.Length - 1)
         {
             _currentGridIndex = 0;
         }
         grids[_currentGridIndex].gameObject.SetActive(true);
   }

   public void OnLeftBtnClick()
   {
       grids[_currentGridIndex].gameObject.SetActive(false);
       _currentGridIndex--;
       if (_currentGridIndex < 0)
       {
           _currentGridIndex = grids.Length - 1;
       }
       grids[_currentGridIndex].gameObject.SetActive(true);
       
   }
}
