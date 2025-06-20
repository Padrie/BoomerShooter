﻿using UnityEngine;

namespace ___Workdata.Scripts.Player.StateMachine.States
{
    public class IdleState : State
    {
        public override void StateUpdate()
        {
            if (playerController.inputX != 0 || playerController.inputZ != 0)
            {
                End("WalkingState");
                return;
            }

            if (Input.GetKeyDown(KeyCode.Space) && playerController.isGrounded)
            {
                End("JumpState");
                return;
            }
        }
    }
}