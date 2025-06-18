using ___WorkData.Scripts.StateMachine;
using UnityEngine;

namespace ___WorkData.Scripts.Statemachine.States.Player
{
    public class IdleState : State
    {
        public State walkState;
        public State runState;
        public State dashState;
        public State crouchState;
        public State jumpState;
        public State fallState;
        
        public override void Update()
        {
           playerController.HandleCameraMovement();

           if (playerController.isGrounded && Input.GetButtonDown("Jump"))
           {
               End(jumpState);
               return;
           }

           if (!playerController.isGrounded && playerController.velocity.y < 0)
           {
               End(fallState);
               return;
           }
           
        }
        
        
    }
}