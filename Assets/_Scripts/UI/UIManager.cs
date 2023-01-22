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
        
        if(data.TryGetValue("moves", out var moves))
        {
            hud.SetMoves(moves.ToString());
        }
    }

    protected override void OnAwake()
    {
        base.OnAwake();

        _popupController = new PopupController();
        _popupController.Init(popupRoot);

        botHUD.SettingsButton.onClick.AddListener(() =>
        {
            ObserverManager.AddData("state", GameState.PAUSE);
            ObserverManager.Notify( ODType.Game);

            _popupController.LoadGOusingAddress();
        });
        
        botHUD.UndoButton.onClick.AddListener(() =>
        {
            ObserverManager.AddData("undo");
            ObserverManager.Notify( ODType.Game);
        });
    }
}