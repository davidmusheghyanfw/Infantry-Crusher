using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseManager : MonoBehaviour
{
    public static BaseManager instance;
    [SerializeField] private List<BaseController> bases;
    private List<BaseController> activeBases;
    private BaseController currentBase;
    private int baseIndex;
    private void Awake() {
        instance = this;
    }

    public void InitBaseController()
    {
        activeBases = new List<BaseController>(); 
        baseIndex = -1;
        for(int i = 0; i<bases.Count; i++)
        {
            if(bases[i].baseState == BaseState.completed)
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
        if(baseIndex + 1 < activeBases.Count) return true;
        return false;
    }
    public bool CheckMinLimit()
    {
        if(baseIndex - 1 >= 0) return true;
        return false;
    }
    public BaseController ShowNextBase()
    {
        if(baseIndex + 1 >= activeBases.Count) return null;
        activeBases[baseIndex].Hide();
        baseIndex++;
        activeBases[baseIndex].Show();
        return activeBases[baseIndex];   
    }
    public BaseController ShowPrevBase()
    {
        if(baseIndex - 1 < 0) return null;
        activeBases[baseIndex].Hide();
        baseIndex--;
        activeBases[baseIndex].Show();
        return activeBases[baseIndex];
    }
}

public enum BaseState {undefined, completed,inProgress, locked }