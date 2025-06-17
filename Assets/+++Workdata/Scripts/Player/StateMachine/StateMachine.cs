using UnityEngine;

namespace ___Workdata.Scripts.Player.StateMachine
{
    public class StateMachine : MonoBehaviour
    {
        public State currentState;
        public State lastState;
    
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            currentState.Start();
            currentState.Enter();
        }

        // Update is called once per frame
        void Update()
        {
            currentState.StateUpdate();
        }

        private void FixedUpdate()
        {
            currentState.StateFixedUpdate();
        }

        private void LateUpdate()
        {
            checkEnd();
        }

        private void checkEnd()
        {
            if (currentState.nextState != null || currentState.getEndCalled())
            {
                lastState = currentState;
                currentState = currentState.nextState;
                
                currentState.Enter();
                lastState.nextState = null;
                currentState.reset();
               
            }
        }
    }
}