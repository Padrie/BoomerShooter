using ___WorkData.Scripts.StateMachine;
using UnityEngine;

namespace ___WorkData.Scripts.Statemachine.States.Player
{
    public class GlideState : State
    { 
        public State idleState;
        public State dashState;
        
        public override void Update()
        {

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