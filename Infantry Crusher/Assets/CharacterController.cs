using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public static CharacterController instance;
    [SerializeField] private Transform player;
    [SerializeField] private Animator animator;
    private void Awake()
    {
        instance = this;
    }

    public void InitCharacterController()
    {

    }

    public void RunToPos()
    {
        Vector3 targetPostition = new Vector3(transform.position.x,
                                      player.position.y,
                                       transform.position.z);
        transform.LookAt(targetPostition);
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
                GameManager.instance.LevelStart();
                break;
            }
            yield return new WaitForEndOfFrame();
        }
    }
}
