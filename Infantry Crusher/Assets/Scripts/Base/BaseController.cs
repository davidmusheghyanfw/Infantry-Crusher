using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseController : MonoBehaviour
{

   public BaseState baseState;
   [SerializeField] private List<BaseElement> baseElements;
   public List<BaseElement> BaseElements{get{return baseElements;}}
   private List<BaseElementData> baseElementsData;
   public List<BaseElementData> BaseElementsDatas{get{return baseElementsData;}}
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

   public void SetData(List<BaseElementData> _baseElementDatas)
   {
      if(_baseElementDatas.Count > 0)
      {
         baseElementsData = _baseElementDatas;
         for (int i = 0; i < baseElementsData.Count; i++)
         {

            baseElements[i].SetFillProgress(_baseElementDatas[i].progress,1f);
         }
      }
      else
      {
         baseElementsData = new List<BaseElementData>();
         for (int i = 0; i < baseElements.Count; i++)
         {
            baseElementsData.Add(new BaseElementData(baseElements[i]));
         }
      }
   }

}
public enum BaseElementState{complite,inPorgress,locked}
