using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public static CharacterController instance;
    private Transform player;
    [SerializeField] private Animator animator;
    [SerializeField] private Transform startPos;
    private void Awake()
    {
        instance = this;
    }

    public void InitCharacterController()
    {
        transform.position = startPos.position;
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
        
        //CameraController.instance.SetAimTarget(CameraState.Follow,transform);
        //Vector3 targetPostition = new Vector3(transform.position.x,
        //                              player.position.y,
        //                               transform.position.z);
        transform.LookAt(player);
        animator.Play("Fast Run");
        StartCoroutine(StopingRoutine());
    }

    private IEnumerator StopingRoutine()
    {
        while (true)
        {
          
            if (Vector3.Distance(transform.position, player.position) < 1)
            {
                animator.Play("Idle");
                GameManager.instance.StageStart();
                break;
            }
            yield return new WaitForEndOfFrame();
        }
    }
}
