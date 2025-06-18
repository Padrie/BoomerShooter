using ___WorkData.Scripts.StateMachine;
using UnityEngine;

namespace ___WorkData.Scripts.Statemachine.States.Player
{
    public class FallState : State
    {

        public State idleState;
        public State dashState;
        public State glideState;
        
        public override void Update()
        {
            if (Input.GetKey(KeyCode.Space) && !playerController.isGrounded)
            {
                End(glideState);
            }

            if (playerController.isGrounded)
            {
                End(idleState);
            }
            
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                End(dashState);
            }
            
        }
    }
}