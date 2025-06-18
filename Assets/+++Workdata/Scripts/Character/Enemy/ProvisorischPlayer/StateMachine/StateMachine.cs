using UnityEngine;

namespace ___WorkData.Scripts.StateMachine
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
            if (currentState.nextState != null)
            {
                lastState = currentState;
                currentState = currentState.nextState;
                
                lastState.nextState = null;
                currentState.Enter();
            }
            Debug.Log(currentState);
            currentState.Update();
            
        }

        private void FixedUpdate()
        {
            currentState.FixedUpdate();
        }
    }
}
