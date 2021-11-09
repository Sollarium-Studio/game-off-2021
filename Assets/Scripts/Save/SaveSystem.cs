using System;
using JetBrains.Annotations;
using UnityEngine;

namespace Save
{
    public class SaveSystem : MonoBehaviour
    {
        #region Singleton

        public static SaveSystem instance;

        private void Awake()
        {
            if (instance) Destroy(gameObject);
            else instance = this;
            DontDestroyOnLoad(gameObject);
        }
        
        #endregion

        public SaveFile Save(string saveName, SaveFile data)
        {
            SaveSerialization.Save(saveName, data);
            return data;
        }
        
        public SaveFile Load(string saveName)
        {
            var fileLoaded =  SaveSerialization.Load($"{Application.persistentDataPath}/saves/{saveName}.sol");
            return fileLoaded;
        }
    }
}
