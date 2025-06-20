using System.Collections.Generic;
using UnityEngine;

namespace ___Workdata.Scripts.Player.StateMachine
{
    public class StateMachine : MonoBehaviour
    {
        
        public PlayerController playerController;
        public State currentState;
        public State lastState;
        public List<State> States;
        
    
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            currentState.Enter();
            
            foreach (var state in States)
            {
                state.playerController = playerController;
            }
        }

        // Update is called once per frame
        void Update()
        {
            currentState.StateUpdate();
        }

        void FixedUpdate()
        {
            currentState.StateFixedUpdate();
        }

        void LateUpdate()
        {
            checkEnd();
        }

        void checkEnd()
        {
            if (currentState.nextState != null || currentState.getEndCalled())
            {
                lastState = currentState;
                currentState = currentState.nextState;
                
                currentState.Enter();
                lastState.nextState = null;
                currentState.Reset();
               
            }
        }
    }
}