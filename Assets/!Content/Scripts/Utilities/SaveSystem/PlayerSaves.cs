namespace Game.Utilities.SaveSystem
{
    using Newtonsoft.Json;
    
    public class PlayerSaves
    {
        private GameSaveSystem _gameSaveSystem;
        private GameProfile _playerProfile;

        public GameProfile PlayerProfile => _playerProfile;

        public PlayerSaves()
        {
            _gameSaveSystem = new GameSaveSystem();
            _playerProfile = LoadOrCreate();
        }
        
        public void Save()
        {
            var json = JsonConvert.SerializeObject(_playerProfile, Formatting.None);
            _gameSaveSystem.Save(json);
        }

        private GameProfile LoadOrCreate()
        {
             var s = new JsonSerializerSettings
             {
                 CheckAdditionalContent = false
             };
            return _gameSaveSystem.TryLoad(out var save) ? JsonConvert.DeserializeObject<GameProfile>(save, s) : CreateProfile();
        }

        private GameProfile CreateProfile()
        {
            var profileBuilder = new GameProfileBuilder();
            return profileBuilder
                .InitializeProperties()
                .InitializeSettings()
                .Build();
        }
    }
}