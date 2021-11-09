using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using JetBrains.Annotations;
using UnityEngine;

namespace Save
{
    public static class SaveSerialization
    {
        public static bool Save(string saveName, SaveFile data)
        {
            var formatter = GetBinaryFormatter();

            if (!Directory.Exists($"{Application.persistentDataPath}/saves"))
                Directory.CreateDirectory($"{Application.persistentDataPath}/saves");

            var path = $"{Application.persistentDataPath}/saves/{saveName}.sol";

            var file = File.Create(path);
            formatter.Serialize(file,data);
            file.Close();

            return true;
        }

        public static SaveFile Load(string path)
        {
            if (!File.Exists(path)) return null;
            var formatter = GetBinaryFormatter();

            try
            {
                var file = File.Open(path, FileMode.Open);
                var save = (SaveFile)formatter.Deserialize(file);
                file.Close();
                return save;
            }
            catch (Exception e)
            {
                Debug.LogError($"Unable to load save in: {path}\nError: {e}");
                return null;
            }
        }


        private static BinaryFormatter GetBinaryFormatter()
        {
            return new BinaryFormatter();
        }
    }
}