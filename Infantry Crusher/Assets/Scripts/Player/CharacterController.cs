using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public static CharacterController instance;
    private Transform player;
    [SerializeField] private Animator animator;
    [SerializeField] private Transform startPos;
    [SerializeField] private CinemachineVirtualCamera camera;
    [SerializeField] private CinemachinePath path;
    private CameraController cameraController;
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        cameraController = CameraController.instance;

        
    }
    public void InitCharacterController()
    {
        transform.position = startPos.position;
        EnemyManager.instance.SetPlayerPos(transform);
        CameraController.instance.GetCameraProperties(CameraState.Follow).camera = camera;
        CameraController.instance.SwitchCamera(CameraState.Follow);
        CameraController.instance.SetFollowTarget(CameraState.Follow, transform);
        CameraController.instance.SetAimTarget(CameraState.Follow, transform);


        CameraController.instance.SetAimTarget(CameraState.End, transform);
        CameraController.instance.SetTrackedDollyCamera(CameraState.End);
        CameraController.instance.SetTrackedDollyPath(CameraState.End, path);
    }

    public void GetNextPoin(Transform value)
    {
        player = value;
    }
    public void RunToPos()
    {
        GameManager.instance.IsPlayerInteractble = false;
        CameraController.instance.SwitchCamera(CameraState.Follow);
        transform.LookAt(player);
        animator.Play("Fast Run");
             
        StartCoroutine(StopingRoutine());
    }

    private IEnumerator StopingRoutine()
    {
        while (true)
        {
            if(player is null)
            {
                animator.Play("Idle");
                break;
            }
            else if (Vector3.Distance(transform.position, player.position) < 1)
            {
                animator.Play("Idle");
                GameManager.instance.StageStart();
                break;
            }
            yield return new WaitForEndOfFrame();
        }
    }

    public void VisualDieAnim()
    {
        animator.Play("Die");
    }
}
