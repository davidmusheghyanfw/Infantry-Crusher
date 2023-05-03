using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class TouchManager : MonoBehaviour
{
    public static TouchManager instance;

    public UnityAction<Vector3, Vector3> OnTouchDrag;
    public UnityAction<Vector3> OnTouchUp;
    public UnityAction<Vector3> OnTouchDown;

    public bool TouchActive { get; set; }
    public Vector3 DeltaPosition
    {
        get { return m_DeltaPosition; }
    }

    Vector3 m_StartPosition;
    Vector3 m_CurrentPosition;
    Vector3 m_LastPosition;
    Vector3 m_DeltaPosition;
    Vector3 m_TotalDeltaPosition;

    public bool isTouchDown = false;

    void Awake()
    {
        instance = this;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            m_StartPosition = Input.mousePosition;
            m_LastPosition = m_StartPosition;
            m_DeltaPosition = Vector3.zero;

            OnTouchDown?.Invoke(m_StartPosition);

            isTouchDown = true;

            //FingerController.instance.Push();
        }
        if (Input.GetMouseButton(0))
        {
            m_CurrentPosition = Input.mousePosition;
            m_DeltaPosition = m_CurrentPosition - m_LastPosition;
            m_LastPosition = m_CurrentPosition;
            m_TotalDeltaPosition = m_CurrentPosition - m_StartPosition;

           
            OnTouchDrag?.Invoke(m_CurrentPosition, m_DeltaPosition);
        }
        if (Input.GetMouseButtonUp(0))
        {
            m_DeltaPosition = Vector3.zero;

            OnTouchUp?.Invoke(m_DeltaPosition);

            isTouchDown = false;

            //FingerController.instance.Release();
        }
    }

  
}
