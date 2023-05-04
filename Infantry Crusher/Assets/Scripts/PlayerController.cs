using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float rotationControll;
    [SerializeField] private Vector2 verticalLimit;
    [SerializeField] private Vector2 horizontalLimit;

    Vector3 deltaPos;
    Vector3 prevDeltaPos = Vector3.zero;
    Vector3 limitsChecker = Vector3.zero;
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
        deltaPos = new Vector3(-cursorPos.y, cursorPos.x, 0) * rotationControll * Time.deltaTime;
        limitsChecker = deltaPos + transform.eulerAngles;
        //if (deltaPos.x + transform.eulerAngles.x < horizontalLimit.x) limitsChecker.x = prevDeltaPos.x;
        //if (deltaPos.x + transform.eulerAngles.x > horizontalLimit.y) limitsChecker.x = prevDeltaPos.y;
        //if (deltaPos.y + transform.eulerAngles.y < horizontalLimit.x) limitsChecker.y = prevDeltaPos.x;
        //if (deltaPos.y + transform.eulerAngles.y > horizontalLimit.y) limitsChecker.y = prevDeltaPos.y;

        transform.eulerAngles = limitsChecker;
       
        //transform.eulerAngles = new Vector3(Mathf.Clamp(transform.eulerAngles.x, -30,30), Mathf.Clamp(transform.eulerAngles.y, -30, 30),0);
        
        prevDeltaPos = limitsChecker;
        MiniGun.instance.Shoot();
    }

    void OnTouchUp(Vector3 lastPos)
    {
        
    }

}
