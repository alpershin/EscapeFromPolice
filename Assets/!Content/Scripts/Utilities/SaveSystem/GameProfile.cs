namespace Game.Utilities.SaveSystem
{
    //using R3;
    using System;
    
    [Serializable]
    public class GameProfile
    {
        // public SerializableReactiveProperty<int> PlayerLevel = new SerializableReactiveProperty<int>(1);
        // public SerializableReactiveProperty<int> PlayerSoftCurrency = new SerializableReactiveProperty<int>(0);
        // public SerializableReactiveProperty<EWeaponType> Weapon = new SerializableReactiveProperty<EWeaponType>(EWeaponType.Pistol);
        public SettingsData Settings = new SettingsData();
    }

    [Serializable]
    public class SettingsData
    {
        // public SerializableReactiveProperty<bool> IsMusicOn = new SerializableReactiveProperty<bool>(true);
        // public SerializableReactiveProperty<bool> IsSoundOn = new SerializableReactiveProperty<bool>(true);
        // public SerializableReactiveProperty<bool> IsVibrationOn = new SerializableReactiveProperty<bool>(true);
    }
}