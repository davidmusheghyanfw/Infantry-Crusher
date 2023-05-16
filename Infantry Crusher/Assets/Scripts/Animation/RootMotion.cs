using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RootMotion : MonoBehaviour
{
    [SerializeField] Transform parent;
    [SerializeField] Animator animator;
    [SerializeField] NavMeshAgent navMesh;
    private Vector3 position;

    void OnAnimatorMove()
    {
        //transform.position = animator.rootPosition;


        //parent.position += animator.deltaPosition;
        position = animator.rootPosition;
        position.y = navMesh.nextPosition.y;
       // transform.position = position;
        navMesh.nextPosition = position;
        parent.position = position;
    }

}
