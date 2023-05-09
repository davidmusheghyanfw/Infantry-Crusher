using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    [SerializeField] private Gun activeGun;

    [SerializeField] private float rotationControll;
    [SerializeField] private Vector2 verticalLimit;
    [SerializeField] private Vector2 horizontalLimit;

    Vector3 deltaPos;
    Vector3 prevDeltaPos = Vector3.zero;
    Vector3 limitsChecker = Vector3.zero;
    private Vector3 cursor;

    private void Awake()
    {
        instance = this;    
    }

    void Start()
    {

        TouchManager.instance.OnTouchDown += OnTouchDown;
        TouchManager.instance.OnTouchDrag += OnTouchDrag;
        TouchManager.instance.OnTouchUp += OnTouchUp;
    }

    void OnTouchDown(Vector3 startPos)
    {
        activeGun.StartVisual();

    }

    void OnTouchDrag(Vector3 currentPos, Vector3 cursorPos)
    {

        cursor = cursorPos;
        deltaPos = new Vector3(-cursorPos.y, cursorPos.x, 0) * rotationControll * Time.deltaTime;
        limitsChecker = deltaPos + transform.eulerAngles;

        //if (limitsChecker.x < horizontalLimit.x) limitsChecker.x = prevDeltaPos.x;
        //if (limitsChecker.x > horizontalLimit.y) limitsChecker.x = prevDeltaPos.y;
        //if (limitsChecker.y < verticalLimit.x) limitsChecker.y = prevDeltaPos.x;
        //if (limitsChecker.y > verticalLimit.y) limitsChecker.y = prevDeltaPos.y;

        transform.eulerAngles = limitsChecker;

        prevDeltaPos = limitsChecker;
        activeGun.Shoot();
    }

    void OnTouchUp(Vector3 lastPos)
    {
        activeGun.StopVisual();
    }

}
