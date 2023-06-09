using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BaseElementData
{
    public float progress;

    public BaseElementData(BaseElement elements)
    {
        progress = elements.Progress;
    }
}
