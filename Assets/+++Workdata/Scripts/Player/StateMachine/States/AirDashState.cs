using UnityEngine;

namespace ___Workdata.Scripts.Player.StateMachine.States
{
    public class AirDashState : State
    {

        public override void StateUpdate()
        {
            StartCoroutine(playerController.HandleAirDash());
            
            if (playerController.isGrounded)
            {
                End("IdleState");
            }else End("FallState");
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