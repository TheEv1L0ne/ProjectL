//TODO: RENAME THIS INTO SOMETHING MORE APPROPRIATE 

using System;
using System.Collections.Generic;
using _Scripts.Helpers.ObserverPattern;
using Newtonsoft.Json.Linq;
using UnityEngine;

public class ObserverManager
{
    private static readonly List<IObserver> Observers = new();

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

    public static void Notify(string data, params object[] receivers)
    {
        if (Observers == null) return;

        foreach (var observer in Observers)
        {
            observer.UpdateState(JObject.Parse(data) , receivers);
        }
    }
}