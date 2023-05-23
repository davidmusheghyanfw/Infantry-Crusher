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
    private void Awake()
    {
        instance = this;
    }

    public void InitCharacterController()
    {
        transform.position = startPos.position;
        EnemyManager.instance.SetPlayerPos(transform);
        CameraController.instance.GetCameraProperties(CameraState.Follow).camera = camera;
        CameraController.instance.SwitchCamera(CameraState.Follow);
        CameraController.instance.SetFollowTarget(CameraState.Follow, transform);
        CameraController.instance.SetAimTarget(CameraState.Follow, transform);
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
}
