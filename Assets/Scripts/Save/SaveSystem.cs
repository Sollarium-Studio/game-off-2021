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
        
        [CanBeNull]
        public SaveFile Load(string saveName)
        {
            return SaveSerialization.Load($"{Application.persistentDataPath}/saves/{saveName}.sol");
        }
    }
}
