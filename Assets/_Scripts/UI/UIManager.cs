using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = System.Object;

public class UIManager : Singleton<UIManager>, IObserver
{
    [SerializeField] private HUD hud;

    private void OnEnable()
    {
        ObserverManager.Instance.Attach(Instance);
    }

    private void OnDisable()
    {
        if(ObserverManager.Instance != null)
            ObserverManager.Instance.Detach(Instance);
    }

    private void NoOfMovesChanged(int moves)
    {
        hud.SetMoves(moves.ToString());
    }

    public void UpdateState(ODType[] type, Object[] data)
    {
        if (!type.Contains(ODType.UI)) return;
        
        var moves = data[0].ToString();
        hud.SetMoves(moves);
    }
}
