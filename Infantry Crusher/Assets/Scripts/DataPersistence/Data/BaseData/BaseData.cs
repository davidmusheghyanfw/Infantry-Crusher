using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BaseData
{
   
    public BaseState state;
    public List<BaseElementData> elementDatas;

    public BaseData(BaseState _state, List<BaseElementData> _elementDatas)
    {
        state = _state;
        elementDatas = _elementDatas;
    }

}
