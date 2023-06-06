using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseController : MonoBehaviour
{
    public static BaseController instance;
    [SerializeField] private List<Base> bases;
    private List<Base> activeBases;
    private Base currentBase;
    private int baseIndex;
    private void Awake() {
        instance = this;
    }

    public void InitBaseController()
    {
        activeBases = new List<Base>(); 
        baseIndex = 0;
        for(int i = 0; i<bases.Count; i++)
        {
            if(bases[i].baseState == BaseState.complited)
            {
                baseIndex++;
                activeBases.Add(bases[i]);
            }   
            if(bases[i].baseState == BaseState.inProgress)
            {
                baseIndex++;
                currentBase = bases[i];
                currentBase.Show();
                activeBases.Add(currentBase);
            }
        }
      
    }

    public int ActiveBasesCount()
    {
        return activeBases.Count;
    }

    public bool CheckMaxLimit()
    {
        if(baseIndex++<activeBases.Count) return true;
        return false;
    }
    public bool CheckMinLimit()
    {
        if(baseIndex-->=0) return true;
        return false;
    }
    public Base ShowNextBase()
    {
        if(baseIndex > activeBases.Count) return null;
        activeBases[baseIndex].Hide();
        baseIndex++;
        activeBases[baseIndex].Show();
        return activeBases[baseIndex];   
    }
    public Base ShowPrevBase()
    {
        if(baseIndex < 0) return null;
        activeBases[baseIndex].Hide();
        baseIndex--;
        activeBases[baseIndex].Show();
        return activeBases[baseIndex];
    }
}

public enum BaseState {undifinde,complited,inProgress,locked }