using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseManager : MonoBehaviour, IDataPersistence
{
    public static BaseManager instance;
    [SerializeField] private List<BaseController> bases;
    private List<BaseController> activeBases;
    private BaseController currentBase;
    private int baseIndex;
    private void Awake() {

        if(instance == null)
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

    public void ChangeCurrentBaseState(BaseState state)
    {
        currentBase.baseState = state;
    }

    public void BaseComplited()
    {
        if(baseIndex+1>= bases.Count) return;

        currentBase.baseState = BaseState.completed;
        currentBase.Hide();
        baseIndex++;
        currentBase = bases[baseIndex];
        currentBase.baseState = BaseState.inProgress;
        currentBase.Show();
        activeBases.Add(currentBase);
    }

    public void DeinitBaseController()
    {
        currentBase.Hide();
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

    public void LoadData(GameData data)
    {
        if(data.basesData.Count !=0)
        {
            for (int i = 0; i < bases.Count; i++)
            {
                bases[i].baseState = data.basesData[i].state;
                for (int j = 0; j < data.basesData[i].elementDatas.Count; j++)
                {
                    bases[i].BaseElements[j].SetFillProgress(data.basesData[i].elementDatas[j].progress,0);
                }
            }
        }
    }

    public void SaveData(ref GameData data)
    {
        
        for (int i = 0; i < bases.Count; i++)
        {
            List<BaseElementData> elementDatas = new List<BaseElementData>();
            for (int j = 0; j < bases[i].BaseElements.Count; j++)
            {
                elementDatas.Add(new BaseElementData(bases[i].BaseElements[j]));
            }
            data.basesData.Add(new BaseData(bases[i].baseState,elementDatas));
            
        }
         
    }
}

public enum BaseState {undefined, completed,inProgress, locked }