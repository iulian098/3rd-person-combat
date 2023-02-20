using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateMachine : MonoBehaviour
{
    private State currentState;

    public void SwitchState(State newState) {
        currentState?.OnExit();
        currentState = newState;
        currentState.OnEnter();
    }

    private void Update() {
        if (currentState != null)
            currentState.OnTick(Time.deltaTime);
    }
}
