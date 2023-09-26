using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateMachine : MonoBehaviour
{
    public class Transition
    {
        readonly public State NextState;
        readonly public Func<bool> Condition;
        public Transition(State nextState, Func<bool> condition)
        {
            NextState = nextState;
            Condition = condition;
        }
    }

    public class State
    {
        readonly public List<Transition> Transitions;
        public State()
        {
            Transitions = new List<Transition>();
        }

        public virtual void Behave(float dt) { }
        public virtual void Begin(StateMachine machine) { }
        public virtual void End(StateMachine machine) { }
    }

    private State current { get; set; }

    private void FixedUpdate()
    {
        current.Behave(Time.deltaTime);
        foreach (var transition in current.Transitions)
        {
            if (transition.Condition())
            {
                ChanegState(transition.NextState);
                break;
            }
        }
    }

    protected void ChanegState(State next)
    {
        current.End(this);
        current = next;
        current.Begin(this);
    }

    protected abstract void ClearData();
    protected abstract void InitializeData();
    protected abstract void BuildTransitions();
    protected abstract State StartState { get; }


    private void Awake()
    {
        ResetMachine();
    }

    public void ResetMachine()
    {
        ClearData();
        InitializeData();

        current = StartState;
        current.Begin(this);

        BuildTransitions();
    }

    readonly public Dictionary<string, object> data = new Dictionary<string, object>();

}