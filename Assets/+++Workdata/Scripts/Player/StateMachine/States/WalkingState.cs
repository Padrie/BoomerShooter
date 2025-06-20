using UnityEngine;

namespace ___Workdata.Scripts.Player.StateMachine.States
{
    public class WalkingState : State
    {
        public override void StateUpdate()
        {
            if (playerController.inputX == 0 && playerController.inputZ == 0)
            {
                End("IdleState");
                return;
            }
            
            if (Input.GetKeyDown(KeyCode.Space) && playerController.isGrounded)
            {
                End("JumpState");
                return;
            }
            
        }

        public override void StateFixedUpdate()
        {
            playerController.HandleMovement();
        }
    }
}