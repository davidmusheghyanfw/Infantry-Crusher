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
        if (navMesh.isActiveAndEnabled)
        {
            position = animator.rootPosition;
            position.y = navMesh.nextPosition.y;

            navMesh.nextPosition = position;
            parent.position = position;
        }
    }

}
