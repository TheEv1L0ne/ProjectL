//TODO: RENAME THIS INTO SOMETHING MORE APPROPRIATE 

using System;
using System.Collections.Generic;

public static class ObserverManager
{
    private static readonly List<IObserver> Observers = new();
    
    public static void Attach(IObserver observer)
    {
        if(Observers != null && !Observers.Contains(observer))
            Observers.Add(observer);
    }

    public static void Detach(IObserver observer)
    {
        if(Observers != null && Observers.Contains(observer))
            Observers.Remove(observer);
    }

    public static void Notify(ODType[] type, Object[] data)
    {
        if (Observers == null) return;
        
        foreach (var observer in Observers)
        {
            observer.UpdateState(type, data);
        }
    }
}
