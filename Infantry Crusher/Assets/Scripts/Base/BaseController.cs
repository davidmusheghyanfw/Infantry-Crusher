using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseController : MonoBehaviour
{

   public BaseState baseState;
   [SerializeField] private List<BaseElement> baseElements;

   private BaseElement currentBaseElement;
 
   public void Show()
   {
    gameObject.SetActive(true);
   }

   public void Hide()
   {
    gameObject.SetActive(false);
   } 
}
public enum BaseElementState{complite,inPorgress,locked}
