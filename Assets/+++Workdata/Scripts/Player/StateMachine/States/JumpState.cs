using UnityEngine;

namespace ___Workdata.Scripts.Player.StateMachine.States
{
    public class JumpState : State
    {
        public override void Enter()
        {
            playerController.HandleJump();
        }

        public override void StateUpdate()
        {
            
            if (playerController.velocity.y < 0)
            {
                End("FallState");
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
    }
}