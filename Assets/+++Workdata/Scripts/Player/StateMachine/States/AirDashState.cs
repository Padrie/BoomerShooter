using UnityEngine;

namespace ___Workdata.Scripts.Player.StateMachine.States
{
    public class AirDashState : State
    {
        private Coroutine running;
        
        public override void Enter()
        {
            playerController.canDash = false;
        }

        public override void StateUpdate()
        {
            running = StartCoroutine(playerController.HandleAirDash());

            if (running == null)
            {
                Debug.Log("not running");
            }
            
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