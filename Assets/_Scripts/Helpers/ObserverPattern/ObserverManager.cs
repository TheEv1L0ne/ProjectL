//TODO: RENAME THIS INTO SOMETHING MORE APPROPRIATE 

using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;

public class ObserverManager
{
    private static readonly List<IObserver> Observers = new();
    
    private static Dictionary<string, object> _data = new();

    public static void Attach(IObserver observer)
    {
        if (Observers != null && !Observers.Contains(observer))
            Observers.Add(observer);
    }

    public static void Detach(IObserver observer)
    {
        if (Observers != null && Observers.Contains(observer))
            Observers.Remove(observer);
    }

    public static void Notify(params object[] receivers)
    {
        if (Observers == null) return;

        if (_data.Count == 0)
        {
            Debug.LogWarning($"Sending No Data!");
        }

        foreach (var observer in Observers)
        {
            observer.UpdateState(JObject.Parse(JsonConvert.SerializeObject(_data)) , receivers);
        }

        ClearData();
    }

    public static void ClearData()
    {
        _data = new Dictionary<string, object>();
    }
    
    public static void AddData(string key, object value)
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