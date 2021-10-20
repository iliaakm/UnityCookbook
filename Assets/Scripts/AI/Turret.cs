using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField]
    Transform weapon;
    [SerializeField]
    Transform target;
    [SerializeField]
    float range = 5f;
    [SerializeField]
    float arc = 45f;

    StateMachine stateMachine;

    private void Start()
    {
        stateMachine = new StateMachine();
        var searching = stateMachine.CreateState("searching");
        searching.onEnter = delegate
        {
            print("Now searhing for the target");
        };
        searching.onFrame = delegate
        {
            var angle = Mathf.Sin(Time.time) * arc / 2f;
            weapon.eulerAngles = Vector3.up * angle;

            float distanceToTarget = Vector3.Distance(transform.position, target.position);
            if (distanceToTarget <= range)
            {
                stateMachine.TransitionTo("aiming");
            }
        };

        var aiming = stateMachine.CreateState("aiming");
        aiming.onFrame = delegate
        {
            weapon.LookAt(target.position);
            float distanceToTarget = Vector3.Distance(transform.position, target.position);
            if(distanceToTarget > range)
            {
                stateMachine.TransitionTo(searching);
            }
        };

        aiming.onEnter = delegate
        {
            print("Target is in range!");
        };

        aiming.onExit = delegate
        {
            print("Target wend out of range!");
        };
    }

    private void Update()
    {
        stateMachine.Update();
    }
}
