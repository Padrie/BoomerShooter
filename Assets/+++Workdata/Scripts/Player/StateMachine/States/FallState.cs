using UnityEngine;

namespace ___Workdata.Scripts.Player.StateMachine.States
{
    public class FallState : State
    {
        public override void StateUpdate()
        {
            if (playerController.isGrounded)
            {
                End("IdleState");
                return;
            }
            
            if (Input.GetKeyDown(KeyCode.LeftShift) && playerController.canDash)
            {
                End("AirDashState");
            }
        }
        
        public override void StateFixedUpdate()
        {
            if (playerController.inputX != 0 || playerController.inputZ != 0)
            {
                playerController.HandleMovement();
            }
        }

        public override void Exit()
        {
            playerController.canDash = true;
        }
    }
}