using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerInventoryManager : Singleton<PlayerInventoryManager>, IObserver
{
    private int _energyCap;
    private int _energyRemaining;
    private int _energyRechargeTime;

    public int EnergyCap => _energyCap;
    public int EnergyRemaining => _energyRemaining;
    
    private void OnEnable()
    {
        ObserverManager.Attach(Instance);
    }

    private void OnDisable()
    {
        ObserverManager.Detach(Instance);
    }
    
    protected override void OnAwake()
    {
        base.OnAwake();
        _energyCap = 200;
        _energyRemaining = 200;
    }

    public void UseEnergy()
    {
        _energyRemaining = Mathf.Clamp(_energyRemaining -= 20, 0, 200);
    }

    public void UpdateState(Dictionary<string, object> data, params object[] receivers)
    {
        if (!receivers.Contains(ODType.Inventory)) return;
        
        if(data.TryGetValue("startLevel", out _))
        {
            UseEnergy();
        }
    }
}
