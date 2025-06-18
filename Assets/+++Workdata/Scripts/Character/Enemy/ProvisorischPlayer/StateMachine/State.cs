using UnityEngine;

namespace ___WorkData.Scripts.StateMachine
{
    public abstract class State : MonoBehaviour
    {
        public State nextState;
        
        public PlayerController playerController;

        public virtual void Enter() {}

        public void Start()
        {
            playerController = GetComponent<PlayerController>();
        }

        public virtual void Update() { }

        public virtual void FixedUpdate() { }

        public virtual void Exit() {}
    
        public void End(State state)
        {

            if (state == null)
            {
                throw new System.ArgumentNullException("State");
            }
            
            Exit();
            nextState = state;
        }
    }
}