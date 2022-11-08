//TODO: RENAME THIS INTO SOMETHING MORE APPROPRIATE 

using System;
using System.Collections;
using System.Collections.Generic;

public class ObserverManager : Singleton<ObserverManager>, ISubject
{
    private List<IObserver> _observers;

    protected override void OnAwake()
    {
        base.OnAwake();
        _observers = new List<IObserver>();
    }

    public void Attach(IObserver observer)
    {
        if(_observers != null && !_observers.Contains(observer))
            _observers.Add(observer);
    }

    public void Detach(IObserver observer)
    {
        if(_observers != null && _observers.Contains(observer))
            _observers.Remove(observer);
    }

    public void Notify(ODType[] type, Object[] data)
    {
        if (_observers == null) return;
        
        foreach (var observer in _observers)
        {
            observer.UpdateState(type, data);
        }
    }
}
