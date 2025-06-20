using System;
using System.IO;
using UnityEngine;

namespace ___Workdata.Scripts.Player.StateMachine
{ 
    public abstract class State : MonoBehaviour
    {
        public State nextState;
        protected bool endCalled;
        public PlayerController playerController;
        private StateMachine stateMachine;  

        public void Start()
        {
            stateMachine = GetComponent<StateMachine>();
        }

        public virtual void Enter() {}

        public virtual void Initialize() { }
    
        public virtual void StateUpdate() { }
    
        public virtual void StateFixedUpdate() { }
    
        public virtual void Exit() {}
        
        public void End(State state)
        {
            endCalled = true;
    
            if (nextState == null)
            {
                if (state == null)
                {
                    throw new ArgumentNullException("State");
                }
                    
                nextState = state;
                Exit();
            }
        }
        
        public void End(string state)
        {
            endCalled = true;
            State nState = GetState(state);
    
            if (nextState == null)
            {
                if (state == null)
                {
                    throw new ArgumentNullException("State");
                }
                
                nextState = nState;
                Exit();
            }
        }
        
        public State GetState(string statename)
        {
            return stateMachine.States.Find(state => state.ToString() == statename);
        }
    
        public void Reset()
        {
            endCalled = false;
        }
    
        public bool getEndCalled() 
        {
            return endCalled;
        }

        public override string ToString()
        {
            return this.GetType().Name;
        }
    }
}

