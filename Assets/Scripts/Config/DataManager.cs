using System;
using Save;
using UnityEngine;

namespace Config
{
    public class DataManager : MonoBehaviour
    {
        #region Singleton

        public static DataManager instance;

        private void Awake()
        {
            if (instance) Destroy(gameObject);
            else instance = this;
            DontDestroyOnLoad(gameObject);
        }

        #endregion

        public SaveFile data;

        public delegate void DelegateUpdateConfig();

        public DelegateUpdateConfig updateConfigurations;

        private void Start()
        {
            GetSave();
        }

        private void GetSave()
        {
            if (!SaveSystem.instance) return;
            var save = SaveSystem.instance.Load("test");
            if (save != null)
            {
                data = save;
            }
            else
            {
                var newSave = new SaveFile();
                SaveSerialization.Save("test", newSave);
            }

            updateConfigurations();
        }
    }
}