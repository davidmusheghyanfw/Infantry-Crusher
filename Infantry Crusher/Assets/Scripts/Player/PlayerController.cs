using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    [SerializeField] private List<Gun> gunList = new List<Gun>();
    [SerializeField] private Gun selectedGun;
    [SerializeField] private Gun activeGun;

    [SerializeField] private float rotationControll;
    public float RotationControll { get { return rotationControll; } set { rotationControll = value; } }

    [SerializeField] private Vector2 verticalLimit;
    [SerializeField] private Vector2 horizontalLimit;

    [SerializeField] private float health = 100f;
    public float Health { get { return health; } }

    Vector3 deltaPos;
    Vector3 prevRot = Vector3.zero;
    Vector3 limitsChecker = Vector3.zero;
    Vector3 overallRot = Vector3.zero;
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

    public void InitPlayer()
    {
        CameraController.instance.SwitchCamera(CameraState.Player);
        CameraController.instance.SetFollowTarget(CameraState.Player, activeGun.transform);
        CameraController.instance.SetAimTarget(CameraState.Player, activeGun.transform);
        //transform.position = new Vector3(activeGun.transform.position.x, transform.position.y, activeGun.transform.position.z);
        CameraController.instance.SwitchCamera(CameraState.Player);
    }
    public void AddNewGun(Transform gunPos)
    {
        gunList.Add(Instantiate(selectedGun, gunPos.position, Quaternion.identity, transform));
    }
    public void ToNextGun(int index)
    {
        activeGun = gunList[index];
        activeGun.BulletControllerInstance = BulletController.instance;
        
        CharacterController.instance.GetNextPoin(activeGun.transform);
        this.Timer(1f, () => { 
        CharacterController.instance.RunToPos();
        
        });
    }
    void OnTouchDown(Vector3 startPos)
    {
        if(GameManager.instance.IsPlayerInteractble)activeGun.StartVisual();
    }

    void OnTouchDrag(Vector3 currentPos, Vector3 cursorPos)
    {
        if (GameManager.instance.IsPlayerInteractble)
        {
            //cursor = cursorPos;
            //deltaPos = new Vector3(-cursorPos.y, cursorPos.x, 0) * rotationControll * Time.deltaTime;
            //limitsChecker = deltaPos + transform.eulerAngles;

            //transform.eulerAngles = limitsChecker;


            deltaPos = new Vector3(-cursorPos.y, cursorPos.x, 0) * rotationControll * Time.deltaTime;
            overallRot += deltaPos;
            if (overallRot.x < verticalLimit.x) overallRot.x = prevRot.x;
            if (overallRot.x > verticalLimit.y) overallRot.x = prevRot.x;
            if (overallRot.y < horizontalLimit.x) overallRot.y = prevRot.y;
            if (overallRot.y > horizontalLimit.y) overallRot.y = prevRot.y;

            activeGun.transform.rotation = Quaternion.Euler(overallRot);
            //CameraController.instance.UpdateCameraRotation(transform.rotation);
            prevRot = overallRot;
            activeGun.Shoot();
        }
    }

    void OnTouchUp(Vector3 lastPos)
    {
        if(activeGun is not null) activeGun.StopVisual();
    }

    public void Damaged(float damage)
    {
        health -= damage;
        GameView.instance.ChangeHealtBarValue();
    }

    public void ClearAllGuns()
    {
        for (int i = 0; i < gunList.Count; i++)
        {
            Destroy(gunList[i].gameObject);
        }
        gunList.Clear();
    }
}
