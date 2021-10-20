using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
    public class State
    {
        public string name;
        public System.Action onFrame;
        public System.Action onEnter;
        public System.Action onExit;

        public override string ToString()
        {
            return name;
        }
    }

    Dictionary<string, State> states = new Dictionary<string, State>();
    public State currentState { get; private set; }
    public State initialState;

    public State CreateState(string name)
    {
        var newState = new State();
        newState.name = name;

        if (states.Count == 0)
        {
            initialState = newState;
        }
        states[name] = newState;

        return newState;
    }

    public void Update()
    {
        if (states.Count == 0 || initialState == null)
        {
            Debug.LogError($"State machine has no states!");
            return;
        }

        if (currentState == null)
        {
            TransitionTo(initialState);
        }

        if (currentState.onFrame != null)
        {
            currentState.onFrame();
        }
    }

    public void TransitionTo(State newState)
    {
        if(newState == null)
        {
            Debug.LogError($"Cannot transition to a null state!");
            return;
        }

        if(currentState != null && currentState.onExit != null)
        {
            currentState.onExit();
        }

        Debug.Log($"Transitioning from {currentState} to {newState}");
        currentState = newState;

        if(newState.onEnter != null)
        {
            newState.onEnter();
        }
    }

    public void TransitionTo(string name)
    {
        if(!states.ContainsKey(name))
        {
            Debug.LogError($"State machine doesn't contains a state named {name}");
            return;
        }

        var newState = states[name];
        TransitionTo(newState);
    }
}
