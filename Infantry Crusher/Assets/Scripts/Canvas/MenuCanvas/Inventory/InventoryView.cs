using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.UI;

public class InventoryView : MonoBehaviour
{
   public static InventoryView instace;
   
   [SerializeField] private Cell cell;
   [SerializeField] private GridLayout gunGrid;

    private void Awake()
    {
       instace = this;
    }


}
public enum InventoryType{unknown,locked,unlocked}