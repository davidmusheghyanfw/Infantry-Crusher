using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootMotion : MonoBehaviour
{
    [SerializeField] Transform parent;
    [SerializeField] Animator animator;
    void OnAnimatorMove()
    {
        parent.position += animator.deltaPosition;
    }

}
