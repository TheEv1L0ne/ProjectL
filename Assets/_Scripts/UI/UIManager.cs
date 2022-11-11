using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = System.Object;

public class UIManager : Singleton<UIManager>, IObserver
{
    [SerializeField] private HUD hud;
    [SerializeField] private BotHUD botHUD;

    private void OnEnable()
    {
        ObserverManager.Attach(Instance);
    }

    private void OnDisable()
    {
        ObserverManager.Detach(Instance);
    }

    private void NoOfMovesChanged(int moves)
    {
        hud.SetMoves(moves.ToString());
    }

    public void UpdateState(ODType[] type, string data)
    {
        if (!type.Contains(ODType.UI)) return;
        
        var moves = data[0].ToString();
        hud.SetMoves(moves);
    }

    protected override void OnAwake()
    {
        base.OnAwake();
        
        botHUD.SettingsButton.onClick.AddListener(() =>
        {
            ObserverManager.Notify(new ODType[] {ODType.Game}, GameState.PAUSE.ToString());
        });
    }
}
