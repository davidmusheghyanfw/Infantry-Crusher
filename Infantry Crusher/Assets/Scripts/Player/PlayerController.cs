using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    [Header("Gun")]
    [SerializeField] private List<Gun> gunList = new List<Gun>();
    [SerializeField] private Gun selectedGun;
    private Gun activeGun;

    [Header("AdditionalGun")]
    [SerializeField] private AdditionalGun selectedAdditionalGun;
    private AdditionalGun activeAdditionalGun;
    private int additionalGunCounter = 0;
    public int AdditionalGunCounter { get { return additionalGunCounter; } }

    [Header("Visual")]
    [SerializeField] private float rotationControll;
    public float RotationControll { get { return rotationControll; } set { rotationControll = value; } }

    [Header("Limits")]
    [SerializeField] private Vector2 verticalLimit;
    [SerializeField] private Vector2 horizontalLimit;

    [Header("Health")]
    [SerializeField] private float maxHealth;
    private float health;
    public float Health { get { return health; } }
    public float MaxHealth { get { return maxHealth; } }

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

        health = maxHealth;
        ResetAdditionalGunCount();
        CameraController.instance.SwitchCamera(CameraState.Player);
        CameraController.instance.SetFollowTarget(CameraState.Player, activeGun.transform);
        CameraController.instance.SetAimTarget(CameraState.Player, activeGun.transform);
        
    }
    public void AddNewGun(Transform gunPos)
    {
        gunList.Add(Instantiate(selectedGun, gunPos.position, Quaternion.identity, transform));
      
    }

    public void AddAdditionalGun()
    {
        activeAdditionalGun = Instantiate(selectedAdditionalGun, Vector3.zero, Quaternion.identity);
        activeAdditionalGun.Hide();
    }
    public AdditionalGun GetAdditionalGun()
    {
        return activeAdditionalGun;
    }
    public void ToNextGun(int index)
    {
        activeGun = gunList[index];
        activeGun.BulletControllerInstance = BulletController.instance;
        activeAdditionalGun.InitAdditionalGun(activeGun.AdditionalGunPos);
        activeAdditionalGun.transform.parent = activeGun.transform;
        activeAdditionalGun.transform.localRotation = Quaternion.Euler(Vector3.zero);
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
        if (GameManager.instance.IsPlayerInteractble)
        {
            if (activeAdditionalGun.IsPreparedToShoot)
            {
                activeAdditionalGun.Shoot();
                activeAdditionalGun.Hide();
                ResetAdditionalGunCount();
            }
        }
    }

    public void Damaged(float damage)
    {
        health -= damage;
        GameView.instance.ChangeHealtBarValue();
        LevelManager.instance.CheckPlayeHealth();
    }


    public void ClearAllGuns()
    {
        for (int i = 0; i < gunList.Count; i++)
        {
            Destroy(gunList[i].gameObject);
        }
        gunList.Clear();
        Destroy(activeAdditionalGun.gameObject);
    }

    public void ResetAdditionalGunCount()
    {
        additionalGunCounter = 0;
        GameView.instance.SetValueAdditionalGunBar(additionalGunCounter);
    }
    public void IncreaseAdditionalGunCounter()
    {
        if (additionalGunCounter >= activeAdditionalGun.ActivateLimit) return;
        additionalGunCounter++;
        GameView.instance.SetValueAdditionalGunBar(additionalGunCounter);
        if (additionalGunCounter >= activeAdditionalGun.ActivateLimit)
        {
            activeAdditionalGun.Show();
           
        }
    }
}
