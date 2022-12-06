using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using _Scripts.Helpers.ObserverPattern;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Object = System.Object;
using Newtonsoft.Json.Linq;

public class UIManager : Singleton<UIManager>, IObserver
{
    [SerializeField] private HUD hud;
    [SerializeField] private BotHUD botHUD;
    [SerializeField] private Transform popupRoot;

    private void OnEnable()
    {
        ObserverManager.Attach(Instance);
    }

    private void OnDisable()
    {
        ObserverManager.Detach(Instance);
    }

    public void UpdateState(JObject data, params object[] receivers)
    {
        if (!receivers.Contains(ODType.UI)) return;
        
        if(data.TryGetValue("moves", out var moves))
        {
            hud.SetMoves(moves.ToString());
        }
    }

    protected override void OnAwake()
    {
        base.OnAwake();

        botHUD.SettingsButton.onClick.AddListener(() =>
        {
            var observerData = new ObserverData();
            observerData.AddData("state", GameState.PAUSE);
            ObserverManager.Notify(observerData.Data, ODType.Game);

            LoadGOusingAddress();
        });
    }

    private GameObject m_myGameObject;

    public void LoadGOusingAddress()
    {
        Addressables.InstantiateAsync("Popups/PopupPauseGame", popupRoot).Completed +=
            OnLoadDone;
    }

    public void OnLoadDone(AsyncOperationHandle<GameObject> obj)
    {
        m_myGameObject = obj.Result;
    }
}