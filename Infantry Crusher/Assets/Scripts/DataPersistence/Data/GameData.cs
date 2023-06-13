using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public List<BaseData> basesData = new List<BaseData>();
    public int level;

    public int scrap;
    public int money;
    public GameData()
    {
        level = 0;
        
        if(basesData.Count!=0)
        {
        for (int i = 0; i < basesData.Count; i++)
        {
            basesData[i].state = BaseState.locked;
            for (int j = 0; j < basesData[i].elementDatas.Count; j++)
            {
                basesData[i].elementDatas[j].progress = 0;
            }
          
        }
            basesData[0].state = BaseState.inProgress;
        }
    }
   
}
