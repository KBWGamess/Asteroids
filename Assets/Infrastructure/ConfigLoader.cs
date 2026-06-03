using System.IO;
using Newtonsoft.Json;
using UnityEngine;

namespace Asteroids.Infrastructure
{
    public class ConfigLoader
    {
        private readonly string _configPath;

        public ConfigLoader()
        {
            _configPath = Path.Combine(Application.streamingAssetsPath, "Configs");
        }

        public T Load<T>(string fileName) where T : new()
        {
            string fullPath = Path.Combine(_configPath, fileName);

            if (!File.Exists(fullPath))
            {
                Debug.LogWarning($"Config not found: {fullPath}. Using defaults.");
                return new T();
            }

            try
            {
                string json = File.ReadAllText(fullPath);
                return JsonConvert.DeserializeObject<T>(json);
            }
            catch (System.Exception e)
            {
                Debug.LogError($"Failed to parse config {fileName}: {e.Message}");
                return new T();
            }
        }
    }
}