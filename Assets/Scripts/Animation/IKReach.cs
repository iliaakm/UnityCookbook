using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class IKReach : MonoBehaviour
{
    [SerializeField]
    Transform target = null;
    [SerializeField]
    AvatarIKGoal goal = AvatarIKGoal.RightHand;
    [Range(0, 1), SerializeField]
    float weight = 0.5f;

    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnAnimatorIK(int layerIndex)
    {
        animator.SetIKPosition(goal, target.position);
        animator.SetIKPositionWeight(goal, weight);
    }
}
