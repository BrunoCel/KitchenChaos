using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedCounterVisual : MonoBehaviour
{
   [SerializeField] private BaseCounter Counter;
   [SerializeField] private GameObject[] counterVisual;
   private void Start()
   {
      Player.Instance.OnSelectedCounterChanged += Player_OnSelectedCounterChanged;
      
   }

   private void Player_OnSelectedCounterChanged(object sender, Player.OnSelectedCounterChangedEventArgs e)
   {
      if (e.selectedCounter == Counter)
      {
         Show();
      }
      else
      {
         Hide();
      }
   }

   private void Show()
   {
      foreach (GameObject gameObject in counterVisual)
      {
         gameObject.SetActive(true);
      }
      
   }

   private void Hide()
   {
      foreach (GameObject gameObject in counterVisual)
      {
         gameObject.SetActive(false);
      }
   }
      
}
