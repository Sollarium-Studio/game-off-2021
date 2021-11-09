using System;
using UnityEngine;

namespace Save
{
    [Serializable]
    public class SaveFile
    {
        public ConfigFile configFile;

        public SaveFile()
        {
            configFile = new ConfigFile();
        }
    }

    [Serializable]
    public class ConfigFile
    {
        public bool invertXAxisMouse;
        public float mouseXSensitivity;
        public float mouseYSensitivity;

        public ConfigFile()
        {
        invertXAxisMouse = false;
        mouseXSensitivity = 20f;
        mouseYSensitivity = 20f;
        }
    }
}
