using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base : MonoBehaviour
{
   public BaseState baseState;

   public void Show()
   {
    gameObject.SetActive(true);
   }

   public void Hide()
   {
    gameObject.SetActive(false);
   } 
}
