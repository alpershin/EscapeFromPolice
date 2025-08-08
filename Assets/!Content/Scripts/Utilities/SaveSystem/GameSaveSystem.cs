namespace Game.Utilities.SaveSystem
{
    using System;
    using System.Text;
    using System.IO;
    using UnityEngine;

    public class GameSaveSystem
    {
        private const string SaveFileName = "save";
        private static string SaveFilePath => MakePersistentFilePath(SaveFileName);
        
        public void Save(string json)
        {
            FileStream myFile = File.Open(SaveFilePath, FileMode.OpenOrCreate);
            
            BinaryWriter binaryfile = new BinaryWriter(myFile);
            binaryfile.Write(StringToBytes(json));
            binaryfile.Close();
            myFile.Close();
        }
        
        public bool TryLoad(out string saveJson)
        {
            if (!File.Exists(SaveFilePath)) 
            {
                saveJson = String.Empty;
                return false;
            }
            
            var stream = File.Open(SaveFilePath, FileMode.Open);
            var reader = new BinaryReader(stream, Encoding.UTF8, false);
            var saveData = reader.ReadBytes(int.MaxValue);
            saveJson = BytesToString(saveData);
            stream.Close();
            reader.Close();

            return true;
        }

        private string BytesToString(byte[] input)
        {
            return Encoding.UTF8.GetString(input);
        }
        
        private byte[] StringToBytes(string input)
        {
            return input == null ? null : Encoding.UTF8.GetBytes(input);
        }
        
        private static string MakePersistentFilePath(string path)
        {
            const string tmpFileExt = ".dat";
            var newPath = Path.Combine(Application.persistentDataPath, path + tmpFileExt);
            
            return newPath;
        }
        
#if UNITY_EDITOR
        [UnityEditor.MenuItem("Game/Delete file \"save.dat\"")]
        public static void DeleteSaveFile()
        {
            if (File.Exists(SaveFilePath))
                File.Delete(SaveFilePath);
            
            PlayerPrefs.DeleteAll();
        }
#endif
    }
}