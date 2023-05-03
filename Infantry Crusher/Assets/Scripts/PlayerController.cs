using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Vector3 cursor;

    void Start()
    {

        TouchManager.instance.OnTouchDown += OnTouchDown;
        TouchManager.instance.OnTouchDrag += OnTouchDrag;
        TouchManager.instance.OnTouchUp += OnTouchUp;
    }

    void OnTouchDown(Vector3 startPos)
    {


    }

    void OnTouchDrag(Vector3 currentPos, Vector3 cursorPos)
    {

        cursor = cursorPos;
        
        Camera.main.transform.Rotate(new Vector3(-cursorPos.y,cursorPos.x,0) * 10 * Time.deltaTime);
    }

    void OnTouchUp(Vector3 lastPos)
    {
        
    }

}
