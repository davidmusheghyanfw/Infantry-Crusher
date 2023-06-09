using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseController : MonoBehaviour
{

   public BaseState baseState;
   [SerializeField] private List<BaseElement> baseElements;
   public List<BaseElement> BaseElements{get{return baseElements;}}
   private BaseElement currentBaseElement;
   public BaseElement CurrentBaseElement {get{return currentBaseElement;}set{currentBaseElement = value;}}
 
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
