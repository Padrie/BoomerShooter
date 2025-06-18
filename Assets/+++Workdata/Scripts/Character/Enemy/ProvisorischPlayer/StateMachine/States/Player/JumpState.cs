using ___WorkData.Scripts.StateMachine;
using UnityEngine;

namespace ___WorkData.Scripts.Statemachine.States.Player
{
    public class JumpState : State
    {
        public State idleState;
        public State fallState;
        public State dashState;

        public override void Enter()
        {
            playerController.HandleJump();
        }

        public override void Update()
        {

            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                End(dashState);
            }
        }

        public override void FixedUpdate()
        {
            if (playerController.velocity.y < 0)
            {
                End(fallState);
            }
        }
    }
}