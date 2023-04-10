using System;
using System.Collections;
using System.Collections.Generic;
using _Scripts.Popups;
using UnityEngine;
using UnityEngine.UI;

public class BotHUD : MonoBehaviour
{
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button undoButton;

    public Button SettingsButton => settingsButton;
    public Button UndoButton => undoButton;

    private void Awake()
    {
        SettingsButton.onClick.AddListener(() =>
        {
            ObserverManager.AddData("state", GameState.PAUSE);
            
            ObserverManager.AddData("popupParams", new PauseGameParams());
            ObserverManager.AddData("popupShowAction", PopupAction.Later);
            
            ObserverManager.Notify( ODType.Game, ODType.UI);
        });
        
        UndoButton.onClick.AddListener(() =>
        {
            ObserverManager.AddData("undo");
            ObserverManager.Notify( ODType.Game);
        });
    }
}
