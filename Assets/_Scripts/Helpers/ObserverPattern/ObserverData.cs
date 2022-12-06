using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

namespace _Scripts.Helpers.ObserverPattern
{
    public class ObserverData
    {
        private Dictionary<string, object> _data = new();
        public string Data => JsonConvert.SerializeObject(_data);
        
        public void AddData(string key, object value)
        {
            if (!_data.ContainsValue(key))
            {
                _data[key] = value;
            }
            else
            {
                Debug.LogWarning($"Data with key: {key} already exists!");
            }
        }
    }
}