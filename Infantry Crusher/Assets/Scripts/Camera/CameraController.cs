using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController instance;
    [SerializeField] private Camera main;
    public Camera Main { get { return main; } }

    private void Awake()
    {
        instance = this;
    }


    public void UpdateCameraRotation(Quaternion rotation)
    {
        main.transform.rotation = rotation;
    }
    
}
