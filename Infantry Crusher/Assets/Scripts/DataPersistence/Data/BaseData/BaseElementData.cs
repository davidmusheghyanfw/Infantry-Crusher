using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BaseElementData
{
    public float progress;
    public BaseElementState state;
    public BaseElementData(BaseElement element)
    {
        progress = element.Progress;
        state = element.state;
    }
}
