using System;
using System.Collections.Generic;
using System.Linq;
using _Scripts.Popups;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Newtonsoft.Json.Linq;

public class UIManager : Singleton<UIManager>, IObserver
{
    [SerializeField] private HUD hud;
    [SerializeField] private BotHUD botHUD;
    [SerializeField] private Transform popupRoot;

    private PopupController _popupController;

    private void OnEnable()
    {
        ObserverManager.Attach(Instance);
    }

    private void OnDisable()
    {
        ObserverManager.Detach(Instance);
    }

    public void UpdateState(Dictionary<string, object> data, params object[] receivers)
    {
        if (!receivers.Contains(ODType.UI)) return;

        BaseParams baseParams = null;
        PopupAction popupAction;
        
        if(data.TryGetValue("moves", out var moves))
        {
            hud.SetMoves(moves.ToString());
        }

        if (data.TryGetValue("popupParams", out var popupParams))
        {
            baseParams = (BaseParams) popupParams;
        }

        if (data.TryGetValue("popupShowAction", out var popupShowAction))
        {
            if(baseParams == null) return;

            popupAction = (PopupAction) popupShowAction;
            
            _popupController.ShowPopup(baseParams, popupAction);
        }

        if (data.TryGetValue("closePopup", out var popupToClose))
        {
            _popupController.RemovePopup((PopupBase)popupToClose);
        }
    }

    protected override void OnAwake()
    {
        base.OnAwake();

        _popupController = new PopupController();
        _popupController.Init(popupRoot);
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            _popupController.RemoveLastPopup();
        }
    }
}