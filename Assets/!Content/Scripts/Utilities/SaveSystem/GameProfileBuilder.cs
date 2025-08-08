namespace Game.Utilities.SaveSystem
{
    using R3;
    
    public class GameProfileBuilder
    {
        private readonly GameProfile _profile = new();

        public GameProfileBuilder InitializeProperties()
        {
            return this;
        }
        
        public GameProfileBuilder InitializeSettings()
        {
            _profile.Settings = new SettingsData();
            return this;
        }
        
        
        public GameProfile Build()
        {
            return _profile;
        }
    }
}