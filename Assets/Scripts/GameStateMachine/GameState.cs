namespace GameStateMachine
{
    public abstract class GameState
    {
        public abstract void Enter();
        public abstract void Update();
        public abstract void Exit();
    }
}