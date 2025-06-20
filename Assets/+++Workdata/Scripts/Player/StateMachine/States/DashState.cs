namespace ___Workdata.Scripts.Player.StateMachine.States
{
    public class DashState : State
    {
        public override void StateUpdate()
        {
            StartCoroutine(playerController.HandleDash());
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