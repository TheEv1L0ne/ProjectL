using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Object = System.Object;

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
    
    public void UpdateState(ODType[] type, string data)
    {
        if (!type.Contains(ODType.UI)) return;
        
        var moves = data;
        Debug.Log($" data[0] --->> {data}");
        hud.SetMoves(moves);
    }

    protected override void OnAwake()
    {
        base.OnAwake();
        
        botHUD.SettingsButton.onClick.AddListener(() =>
        {
            ObserverManager.Notify(new ODType[] {ODType.Game}, GameState.PAUSE.ToString());
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
