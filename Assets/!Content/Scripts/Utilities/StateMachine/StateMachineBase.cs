namespace Game.Utilities.StateMachine
{
    using System;
    using System.Collections.Generic;
    using R3;
    
    public abstract class StateMachineBase : IDisposable
    {
        protected readonly CompositeDisposable _lifetimeDisposable = new CompositeDisposable();

        private readonly Dictionary<Type, IStateBase> _states = new Dictionary<Type, IStateBase>();
        private IStateBase _currentState = null;

        protected StateMachineBase()
        {
            Observable
                .EveryUpdate()
                .Subscribe(_ => Tick())
                .AddTo(_lifetimeDisposable);
        }
        
        public void Dispose()
        {
            _lifetimeDisposable?.Dispose();
        }

        public void EnterIn<T>()
        {
            if (!_states.TryGetValue(typeof(T), out var enterState))
                throw new Exception($"State {typeof(T).FullName} is not contained in the repository {_states}");

            _currentState?.Exit();
            _currentState = enterState;
            _currentState.Enter();
        }
        
        public void EnterIn(Type type)
        {
            if (!_states.TryGetValue(type, out var enterState))
                throw new Exception($"State {type.FullName} is not contained in the repository {_states}");

            //Debug.Log($"Enter in {type} state");
            _currentState?.Exit();
            _currentState = enterState;
            _currentState.Enter();
        }

        public virtual void Tick() => _currentState?.Update();
        public void Exit() => _currentState?.Exit();

        public bool ContainsStateOf<T>() where T : IStateBase
        {
            if (!_states.TryGetValue(typeof(T), out var enterState))
                throw new Exception($"State {typeof(T).FullName} is not contained in the repository {_states}");

            return true;
        }


        protected void AddState<T>(T stateBase) where T : IStateBase
        {
            var type = typeof(T);
            _states[type] = stateBase;
        }

        protected abstract void InitializeStates();
    }
}