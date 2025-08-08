namespace Game.Utilities.StateMachine
{
    public interface IStateBase
    {
        void Enter();
        void Update();
        void Exit();
    }
}