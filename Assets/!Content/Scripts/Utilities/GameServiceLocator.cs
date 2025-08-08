namespace Game.Core
{
    using System;
    using System.Collections.Generic;
    
    public class GameServiceLocator
    {
        private Dictionary<Type, object> _services = new Dictionary<Type, object>();
        
        public static GameServiceLocator Instance { get; private set; }
        
        public GameServiceLocator()
        {
            if (Instance != null) return;
            Instance = this;
        }

        public void AddService<T>(T serviceInstance)
        {
            _services.TryAdd(typeof(T), serviceInstance);
        }

        public T Resolve<T>()
        {
            if (_services.TryGetValue(typeof(T), out var serviceInstance))
            {
                return (T)serviceInstance;
            }
            
            throw new KeyNotFoundException($"Service {typeof(T)} not found");
        }
    }
}